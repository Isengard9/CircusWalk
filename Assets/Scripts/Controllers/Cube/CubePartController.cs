using DG.Tweening;
using Game.General.Managers;
using UnityEngine;

namespace General.Controllers.Cube
{
    
    public class CubePartController : Controller
    {
        private Sequence movingSequence;
        private Vector3 movePosition;
        private Color color;
        protected override void OnStart()
        {
            base.OnStart();
            
        }

        public void SetColor(Color newColor)
        {
            this.GetComponent<MeshRenderer>().material.color = newColor;
            color = newColor;
        }
        
        public void StartMove()
        {
            var newPosition = (int)transform.localPosition.x;

            newPosition *=-1;

            movePosition = transform.localPosition;
            movePosition.x = newPosition;

            var positions =new  Vector3[3];

            positions[0] = positions[2] = transform.localPosition;
            positions[1] = movePosition;
            
            movingSequence = DOTween.Sequence();
            movingSequence.Append(this.transform.DOPath(positions,3,PathType.Linear,PathMode.Full3D).SetEase(Ease.Linear).SetLoops(-1));
        }

        private void ToOppoSiteMove()
        {
            var newPosition = (int)this.transform.localPosition.x;

            newPosition *=-1;

            movePosition = this.transform.localPosition;
            movePosition.x = newPosition;
                
           // movingSequence.Append(this.transform.DOLocalMove(movePosition,1).SetEase(Ease.Linear).OnComplete(ToOppoSiteMove));
        }

        public void StopMove()
        {
            movingSequence.Kill(false);
        }

        public OddCube CalculateThePart(OddCube oddCube,CubePartController lastCube)
        {
            oddCube.Color = this.color;
            var distance = this.transform.localPosition.x - lastCube.transform.position.x;
            var gameOverState = lastCube.transform.localScale.x - Mathf.Abs(distance) <= 0.0f ? true : false;
            
            if (gameOverState)
            {
               
                oddCube.Scale = this.transform.localScale;
                oddCube.Position = this.transform.position;
                oddCube.Create = true;
                ManagerContainer.Instance.GameManager.GameEnded?.Invoke(true);
            }

            if (Mathf.Abs(distance) <= CubeController.AvarageDistance)
            {
                this.transform.position = lastCube.transform.position + Vector3.forward * 5;
                oddCube.Create = false;
                ManagerContainer.Instance.SoundManager.PlayEffect(true);
                return oddCube;
            }

            oddCube.Create = true;
            var newScaleX = lastCube.transform.localScale.x - Mathf.Abs(distance);
            

            var newPositionX = lastCube.transform.localPosition.x + (distance / 2);

            var newScale = this.transform.localScale;
            newScale.x = newScaleX;
            this.transform.localScale = newScale;

            var newPosition = this.transform.localPosition;
            newPosition.x = newPositionX;
            this.transform.localPosition = newPosition;

            var oddCubeScaleX = lastCube.transform.localScale.x - newScaleX;
            oddCube.Scale = new Vector3(oddCubeScaleX, 1, 5);

            var isOnLeft = distance < 0 ? true : false;

            if (isOnLeft)
            {
                var oddCubePositionX = newPositionX - (newScaleX/2) - (oddCubeScaleX/2);
                oddCube.Position = new Vector3(oddCubePositionX, 0, this.transform.localPosition.z);
            }

            else
            {
                var oddCubePositionX = newPositionX + (newScaleX/2) + (oddCubeScaleX/2);
                oddCube.Position = new Vector3(oddCubePositionX, 0, this.transform.localPosition.z);
            }
            
            ManagerContainer.Instance.SoundManager.PlayEffect(false);
            return oddCube;
        }
    }
}