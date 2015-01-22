using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Model;
using Animation;

namespace Controller
{
    public class DealerController : MonoBehaviour
    {
        public GameObject CardPrefab;
        public GameObject HandGameObject;
        public int HorizontalOffset;
        public int VerticalOffset;

        public Dealer Dealer { set; get; }

        private GameObject secondCardGameObject;

        public IEnumerator AddCard(Card card)
        {
            Dealer.Hand.AddCard(card);
            GameObject cardGameObject = 
                Instantiate(CardPrefab, gameObject.transform.position, Quaternion.identity) 
                as GameObject;
            cardGameObject.transform.SetParent(HandGameObject.transform);
            if (Dealer.Hand.Size != 2)
            {
                cardGameObject.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Textures/Cards/" + card.ToString());
            } else
            {
                secondCardGameObject = cardGameObject;
            }
            Vector3 position = transform.position;
            position.x += (Dealer.Hand.Size - 1) * HorizontalOffset;
            position.y -= VerticalOffset;
            yield return StartCoroutine(cardGameObject.GetComponent<Lerp>().StartLerping(position));
        }

        public GameAction DecideAction(Hand playerHand)
        {
            int handValue = GameController.CalculateHand(Dealer.Hand);
            if (handValue > 16)
            {
                return GameAction.Stand;
            } else
            {
                return GameAction.Hit;
            }
        }

        public void showSecondCard()
        {
            secondCardGameObject.GetComponent<Image>().overrideSprite = 
                Resources.Load<Sprite>("Textures/Cards/" + Dealer.Hand.Cards [1].ToString());
        }
    }
}
