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

        public void AddCard(Card card)
        {
            Dealer.Hand.AddCard(card);
            GameObject cardGameObject = 
                Instantiate(CardPrefab, gameObject.transform.position, Quaternion.identity) 
                as GameObject;
            cardGameObject.transform.SetParent(HandGameObject.transform);
            if (Dealer.Hand.Size != 2)
            {
                cardGameObject.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Textures/Cards/" + card.ToString());
            }
            Vector3 position = transform.position;
            position.x += (Dealer.Hand.Size - 1) * HorizontalOffset;
            position.y -= VerticalOffset;
            cardGameObject.GetComponent<Lerp>().StartLerping(position);
        }
    }
}
