using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Model;
using Animation;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        public Transform DealerControllerTransform;
        // Views
        public GameObject CardPrefab;
        public GameObject BetGameObject;
        public GameObject HandGameObject;
        // Models
        public Player Player { set; get; }

        public int HorizontalOffset;

        void Start()
        {
            updateBetText();
        }

        public IEnumerator AddCard(Card card)
        {
            Player.Hand.AddCard(card);
            GameObject cardGameObject = 
                Instantiate(CardPrefab, DealerControllerTransform.position, Quaternion.identity) 
                as GameObject;
            cardGameObject.transform.SetParent(HandGameObject.transform);
            cardGameObject.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Textures/Cards/" + card.ToString());
            Vector3 position = transform.position;
            position.x += (Player.Hand.Size - 1) * HorizontalOffset;
            yield return StartCoroutine(cardGameObject.GetComponent<Lerp>().StartLerping(position));
        }

        public void BetCash(int amount)
        {
            Player.BetCash(amount);
            updateBetText();
        }

        private void updateBetText()
        {
            BetGameObject.GetComponent<Text>().text = Player.Bet + "";
        }
    }
}
