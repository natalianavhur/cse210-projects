using System;
using System.Collections.Generic;

class Word
{
    public string _text { get; set; }
    public bool _isHidden { get; set; }

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }
    public override string ToString()
    {
        return _isHidden ? "___" : _text;
    }
}



