using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NEWFRAMEWORK: Linkar Framework Libraries
using Linkar.Functions;

namespace LinkarDemoApp
{
    public class LinkarDataTypes
    {

        #region CONVERSIONES

        public static string ConvertToHTML(string value, bool replaceMV = false, bool replaceSV = false)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '\r')
                {
                    int pos2 = i + 1;
                    if (pos2 < value.Length)
                    {
                        if (value[pos2] == '\n')
                        {
                            i++;
                            sb.Append((char)251);
                        }
                        else
                            sb.Append((char)251);
                    }
                }
                else if (value[i] == '\n')
                    sb.Append((char)251);
                else switch (value[i])
                    {
                        case (char)255: sb.Append("&yuml;"); break;
                        case (char)254: sb.Append("&thorn;"); break;
                        case (char)253:
                            if (replaceMV)
                                sb.Append("&yacute;");
                            else
                                sb.Append(value[i]);
                            break;
                        case '€': sb.Append("&euro;"); break;
                        default:
                            if (value[i] >= 32 && value[i] < 253)
                                sb.Append(value[i]);
                            break;
                    }

            }

            string resultado = sb.ToString();
            return resultado;
        }

        public static string ConvertFromHTML(string value, bool replaceMV = false, bool replaceSV = false)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '&')
                {
                    int len = 7;
                    if (i + 7 > value.Length)
                        len = value.Length - i;
                    string token = value.Substring(i, len);
                    if (token.StartsWith("&yuml;"))
                    {
                        sb.Append((char)255);
                        i += 5;
                    }
                    else if (token.StartsWith("&thorn;"))
                    {
                        sb.Append((char)254);
                        i += 6;
                    }
                    else if (token.StartsWith("&euro;"))
                    {
                        sb.Append("€");
                        i += 5;
                    }
                    else if (replaceMV && token.StartsWith("&yacute;"))
                    {
                        sb.Append((char)253);
                        i += 7;
                    }
                    else
                    {
                        sb.Append(value[i]);
                    }
                }
                else if (value[i] == (char)251)
                    sb.Append("\r\n");
                else
                    sb.Append(value[i]);
            }

            string resultado = sb.ToString();
            return resultado;
        }

        #endregion

        #region RAW
        public static string GetRaw(string rawValue, int posVM = -1, int posSM = -1)
        {
            return GetValueText(rawValue, posVM, posSM);
        }

        public static void SetRaw(ref string rawValue, string realValue, int posVM = -1, int posSM = -1)
        {
            if (!string.IsNullOrWhiteSpace(realValue))
                SetValueText(ref rawValue, realValue, posVM, posSM);
            else
                SetValueText(ref rawValue, "", posVM, posSM);
        }


        #endregion

        #region ALPHA
        public static string GetAlpha(string rawValue, int posVM = -1, int posSM = -1)
        {
            string realValue = ConvertFromHTML(GetValueText(rawValue, posVM, posSM), (posVM > -1), (posSM > -1));
            return realValue;
        }

        public static void SetAlpha(ref string rawValue, string realValue, int posVM = -1, int posSM = -1)
        {
            if (!string.IsNullOrWhiteSpace(realValue))
                SetValueText(ref rawValue, ConvertToHTML(realValue, posVM > -1, posSM > -1), posVM, posSM);
            else
                SetValueText(ref rawValue, "", posVM, posSM);
        }

        #endregion

        #region INTEGER
        public static int GetInteger(string rawValue, int posVM = -1, int posSM = -1)
        {
            string realValue = GetValueText(rawValue, posVM, posSM);
            int response = 0;
            if (!string.IsNullOrWhiteSpace(realValue))
                if (!int.TryParse(realValue, out response))
                    response = 0;
            return response;
        }

        public static void SetInteger(ref string rawValue, int realValue, int posVM = -1, int posSM = -1)
        {
            SetValueText(ref rawValue, realValue.ToString(), posVM, posSM);
        }
        #endregion

        #region BOOL
        public static bool GetBool(string rawValue, string propBoolTrue, int posVM = -1, int posSM = -1)
        {
            return GetValueText(rawValue, posVM, posSM) == propBoolTrue ? true : false;
        }

        public static void SetBool(ref string rawValue, bool realValue, string propBoolTrue, string propBoolFalse, int posVM = -1, int posSM = -1)
        {
            SetValueText(ref rawValue, realValue ? propBoolTrue : propBoolFalse, posVM, posSM);
        }
        #endregion

        #region DECIMAL
        public static double GetDecimal(string rawValue, int posVM = -1, int posSM = -1)
        {
            string realValue = GetValueText(rawValue, posVM, posSM);
            double response = 0.0;
            if (!string.IsNullOrWhiteSpace(realValue))
                if (!double.TryParse(realValue, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("en"), out response))
                    response = 0.0;
            return response;
        }

        public static void SetDecimal(ref string rawValue, double realValue, int decimalLength = -1, int posVM = -1, int posSM = -1)
        {
            if (decimalLength < 0)
                SetValueText(ref rawValue, realValue.ToString(new System.Globalization.CultureInfo("en")), posVM, posSM);
            else
                SetValueText(ref rawValue, (Math.Round(realValue, decimalLength)).ToString(new System.Globalization.CultureInfo("en")), posVM, posSM);
        }
        #endregion

        #region DATETIME

        public static string GetDateTime(string rawValue, int posVM = -1, int posSM = -1)
        {
            return GetValueText(rawValue, posVM, posSM);
        }

        public static void SetDateTime(ref string rawValue, string realValue, int posVM = -1, int posSM = -1)
        {
            SetValueText(ref rawValue, realValue, posVM, posSM);
        }

        #endregion

        #region GET_METHODS

        private static string GetValueMark(string text, char mark, int pos)
        {
            string[] array = text.Split(mark);
            if (pos < array.Length && pos != -1)
                return array[pos];
            else
                return "";
        }

        private static string GetValueText(string text, int posVM = -1, int posSM = -1)
        {
            string response = "";
            if (posVM == -1 && posSM == -1)
                response = text;
            else if (posVM != -1 && posSM == -1)
                response = GetValueVM(text, posVM);
            else
                response = GetValueSM(text, posVM, posSM);

            return response;
        }

        private static string GetValueVM(string text, int pos)
        {
            return GetValueMark(text, DBMV_Mark.VM, pos);
        }

        private static string GetValueSM(string text, int posVM, int posSM)
        {
            string textVM = GetValueVM(text, posVM);
            if (textVM != "")
                return GetValueMark(textVM, DBMV_Mark.SM, posSM);
            else
                return textVM;
        }

        #endregion

        #region SET_METHODS

        private static void SetValueMark(ref string returnText, string text, char mark, int pos)
        {
            if (!string.IsNullOrEmpty(text))
                returnText += (pos > 0 ? mark.ToString() : "") + text;
            else
                returnText += (pos > 0 ? mark.ToString() : "");
        }

        private static void SetValueText(ref string returnText, string text, int posVM = -1, int posSM = -1)
        {
            if (posVM == -1 && posSM == -1)
                returnText = text;
            else if (posVM != -1 && posSM == -1)
                SetValueVM(ref returnText, text, posVM);
            else
                SetValueSM(ref returnText, text, posVM, posSM);
        }

        private static void SetValueVM(ref string returnText, string text, int pos)
        {
            SetValueMark(ref returnText, text, DBMV_Mark.VM, pos);
        }

        private static void SetValueSM(ref string returnText, string text, int posVM, int posSM)
        {
            SetValueVM(ref returnText, text, posVM);
            if (returnText != "")
                SetValueMark(ref returnText, returnText, DBMV_Mark.SM, posSM);
        }

        #endregion

    }
}
