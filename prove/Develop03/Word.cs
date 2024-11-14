using System;
using System.Collections.Generic;


public class Word
{
    private string _text;
    public string Text
    {
        get => _text;
        private set => _text = value;
    }

    private bool _isHidden;
    public bool IsHidden
    {
        get => _isHidden;
        private set => _isHidden = value;
    }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? "___" : Text;
    }
}




