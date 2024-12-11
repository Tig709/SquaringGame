using TMPro;
using UnityEngine;

namespace Cards
{
    /// <summary>
    /// Class to make cards, cards can only have values of normal cards. Cards are modular
    /// </summary>
    public class Card : MonoBehaviour
    {
        private enum AllowedSymbols
        {
            Hearts,
            Diamonds,
            Clovers,
            Spades
        }

        private enum AllowedNumbers
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        [SerializeField] private AllowedSymbols selectedSymbol;
        [SerializeField] private AllowedNumbers selectedNumber;

        [SerializeField] private TextMeshProUGUI[] numbersText;
        [SerializeField] private TextMeshProUGUI symbolText;

        private Color _textColor;
        private string _number;
        private string _symbol;
        private int _value;
        public string Number => _number;
        public int Value => _value;
        public string Symbol => _symbol;

        /// <summary>
        /// On changes in editor, change the card type so that you do not have to change multiple things when making 1 change.
        /// </summary>
        private void OnValidate()
        {
            SetTextAndColor();
        }

        private void Start()
        {
            SetTextAndColor();
        }

        private void SetTextAndColor()
        {
            switch (selectedSymbol)
            {
                case AllowedSymbols.Hearts:
                    _symbol = "H";
                    _textColor = Color.red;
                    break;
                case AllowedSymbols.Diamonds:
                    _symbol = "D";
                    _textColor = Color.red;
                    break;
                case AllowedSymbols.Clovers:
                    _symbol = "C";
                    _textColor = Color.black;
                    break;
                case AllowedSymbols.Spades:
                    _symbol = "S";
                    _textColor = Color.black;
                    break;
            }

            SetTextAndColor(symbolText, _symbol);

            switch (selectedNumber)
            {
                case AllowedNumbers.Two:
                    _number = "2";
                    _value = 2;
                    break;
                case AllowedNumbers.Three:
                    _number = "3";
                    _value = 3;
                    break;
                case AllowedNumbers.Four:
                    _number = "4";
                    _value = 4;
                    break;
                case AllowedNumbers.Five:
                    _number = "5";
                    _value = 5;
                    break;
                case AllowedNumbers.Six:
                    _number = "6";
                    _value = 6;
                    break;
                case AllowedNumbers.Seven:
                    _number = "7";
                    _value = 7;
                    break;
                case AllowedNumbers.Eight:
                    _number = "8";
                    _value = 8;
                    break;
                case AllowedNumbers.Nine:
                    _number = "9";
                    _value = 9;
                    break;
                case AllowedNumbers.Ten:
                    _number = "10";
                    _value = 10;
                    break;
                case AllowedNumbers.Jack:
                    _number = "J";
                    _value = 11;
                    break;
                case AllowedNumbers.Queen:
                    _number = "Q";
                    _value = 12;
                    break;
                case AllowedNumbers.King:
                    _number = "K";
                    _value = 13;
                    break;
                case AllowedNumbers.Ace:
                    _number = "A";
                    _value = 14;
                    break;
            }

            foreach (var numberText in numbersText)
                SetTextAndColor(numberText, _number);
        }

        private void SetTextAndColor(TextMeshProUGUI targetText, string textToSet)
        {
            targetText.text = textToSet;
            targetText.color = _textColor;
        }
    }
}
