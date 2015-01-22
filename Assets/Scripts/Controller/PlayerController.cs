using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Model;
using Animation;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        public DealerController DealerController;
        // Views
        public GameObject CardPrefab;
        public GameObject BetGameObject;
        public GameObject HandGameObject;
        // Models
        public Player Player { set; get; }

        public int HorizontalOffset;

        void Awake()
        {
        }

        public void AddCard(Card card)
        {
            Player.Hand.AddCard(card);
            GameObject cardGameObject = 
                Instantiate(CardPrefab, DealerController.gameObject.transform.position, Quaternion.identity) 
                as GameObject;
            cardGameObject.transform.SetParent(HandGameObject.transform);
            cardGameObject.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Textures/Cards/" + card.ToString());
            Vector3 position = transform.position;
            position.x += (Player.Hand.Size - 1) * HorizontalOffset;
            cardGameObject.GetComponent<Lerp>().StartLerping(position);
        }
    }
}
