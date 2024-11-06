using TMPro;
using UnityEngine;

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

    private string _number;
    private string _symbol;
    public string Number => _number;
    public string Symbol => _symbol;

    [SerializeField] private TextMeshProUGUI[] numbersText;
    [SerializeField] private TextMeshProUGUI symbolText;

    private Color _textColor;

    private void OnValidate()
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
                break;
            case AllowedNumbers.Three:
                _number = "3";
                break;
            case AllowedNumbers.Four:
                _number = "4";
                break;
            case AllowedNumbers.Five:
                _number = "5";
                break;
            case AllowedNumbers.Six:
                _number = "6";
                break;
            case AllowedNumbers.Seven:
                _number = "7";
                break;
            case AllowedNumbers.Eight:
                _number = "8";
                break;
            case AllowedNumbers.Nine:
                _number = "9";
                break;
            case AllowedNumbers.Ten:
                _number = "10";
                break;
            case AllowedNumbers.Jack:
                _number = "J";
                break;
            case AllowedNumbers.Queen:
                _number = "Q";
                break;
            case AllowedNumbers.King:
                _number = "K";
                break;
            case AllowedNumbers.Ace:
                _number = "A";
                break;
        }

        foreach (var numberText in numbersText)
        {
            SetTextAndColor(numberText, _number);
        }
    }

    private void SetTextAndColor(TextMeshProUGUI targetText, string textToSet)
    {
        targetText.text = textToSet;
        targetText.color = _textColor;
    }
}
