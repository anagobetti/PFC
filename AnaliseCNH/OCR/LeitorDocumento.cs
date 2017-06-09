using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LeitorDocumento
{
    public class Leitor
    {
        private Tesseract _ocr;
        private Tesseract _ocrNumero;
        public Leitor()
        {
            _ocr = new Tesseract(@"C:\Emgu\emgucv-windows-universal 3.0.0.2157\Emgu.CV.OCR\tessdata", "eng", OcrEngineMode.Default);
            _ocr.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            _ocrNumero = new Tesseract(@"C:\Emgu\emgucv-windows-universal 3.0.0.2157\Emgu.CV.OCR\tessdata", "eng", OcrEngineMode.Default);
            _ocrNumero.SetVariable("tessedit_char_whitelist", "1234567890.");
        }

        public Tesseract.Character[] Reconhecer(Image<Gray, byte> gray)
        {
            _ocr.Recognize(gray);
            return _ocr.GetCharacters();
        }

        public string ObterTexto()
        {
            string text = _ocr.GetText();
            string replacement = Regex.Replace(text, @"\t|\n|\r", "");
            return replacement;
        }
        public string ObterDocumento()
        {
            string text = _ocrNumero.GetText();
            string replacement = Regex.Replace(text, @"\t|\n|\r", "");
            return replacement;
        }

        public Tesseract.Character[] Reconhecer(Image<Gray, byte> image, int tipo)
        {
            if (tipo == 1)
            {
                _ocrNumero.Recognize(image);
                return _ocrNumero.GetCharacters();
            }
            else
            {
                _ocr.Recognize(image);
                return _ocr.GetCharacters();
            }
        }

        public string ObterTexto(int tipo)
        {
            if (tipo == 1)
            {
                return ObterDocumento();

            }
            else
            {
                return ObterTexto();

            }
        }
    }
}
