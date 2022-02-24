using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//By Ronaldo Bonilla 02/15/2022

namespace ProgramaCalculo
{
  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            TextRange range = new TextRange(richTextBoxEntrada.Document.ContentStart, richTextBoxEntrada.Document.ContentEnd);

            string cadena = range.Text;

            cadena = cadena.Replace(" ", "");
            cadena = cadena.Replace("\n", "").Replace("\r", "");

            char[] charArr = cadena.ToCharArray();           

            if(charArr[0] != '[' || charArr[1] != '[')
            {
                MessageBox.Show("ValueError");

                return;
            }

            int lastIndex = charArr.Length - 1;

            if (charArr[lastIndex] != ']' || charArr[lastIndex - 1] != ']')
            {
                MessageBox.Show("ValueError");

                return;
            }

            cadena = cadena.Remove(lastIndex - 1, 1);

            cadena = cadena.Remove(0, 1);


            string[] arreglos = cadena.Split(']');

            int fila = 0;
            int col = 0;

            foreach (string filas in arreglos)
            {
               
                string[] valoresC = filas.Split(',');

                if(valoresC.Length > 1)
                {
                    fila = fila + 1;


                    if(col == 0)
                    {
                        foreach (string columnas in valoresC)
                        {
                            col = col + 1;
                        }
                    }
                   
                }
               

            }

            var numberRow = 0;

            string[,] matrizResult = new string[fila,col];

            foreach (string filas in arreglos)
            {

                string[] valoresC = filas.Split(',');

                var numberCol = 0;

                foreach (string columnas in valoresC)
                {
                    if(columnas == "")
                    {
                        continue;
                    }

                    var validate = System.Text.RegularExpressions.Regex.Matches(columnas, @"[a-zA-Z]").Count;

                    if(validate > 0)
                    {
                        char[] charArrV = columnas.ToCharArray();

                        var value = columnas;

                        if (charArrV[0] == '[')
                        {
                             value = columnas.Remove(0, 1);

                        }

                        if (value[0] == '"')
                        {
                             value = value.Remove(0, 1);

                        }

                        int lastIndexR = value.Length - 1;

                        if (value[lastIndexR] == '"')
                        {
                            value = value.Remove(lastIndexR, 1);

                        }

                        matrizResult[numberRow, numberCol] = value;


                    }
                    else
                    {
                        var value = columnas.Remove(0, 1);

                        if (value[0] == '[')
                        {
                            value = columnas.Remove(0, 1);

                        }

                        if (value[0] == '"')
                        {
                            value = value.Remove(0, 1);

                        }

                        int lastIndexR = value.Length - 1;

                        if (value[lastIndexR] == '"')
                        {
                            value = value.Remove(lastIndexR, 1);

                        }

                        matrizResult[numberRow, numberCol] = value;
                     

                    }

                    numberCol += 1;
                }

                numberRow += 1;

            }


            //VALIDAR NUMEROS

            string[,] matrizResultNumber = new string[fila, col];


            for (int f = 0; f < fila; f++)
            {
                for (int c = 0; c < col; c++)
                {
                    var validateLeter = System.Text.RegularExpressions.Regex.Matches(matrizResult[f,c], @"[a-zA-Z]").Count;

                    if (validateLeter > 0)
                    {
                        //VALIDAR FUNCIONES

                        //VALIDAR PRIMER VALOR
                        char[] charArrVen = matrizResult[f, c].ToCharArray();
                        if (charArrVen[0] != '=')
                        {
                            MessageBox.Show("ValueError");

                            return;

                        }

                        //VALIDAR SEGUNDO VALOR
                        string segundoValor = charArrVen[1].ToString();

                        validateLeter = System.Text.RegularExpressions.Regex.Matches(segundoValor, @"[a-zA-Z]").Count;
                        if (validateLeter > 0)
                        {
                            //VALIDAR SEGUNDO VALOR
                            var stringNumber = charArrVen[2].ToString(); ;
                            int numericValue;
                            bool isNumber = int.TryParse(stringNumber, out numericValue);
                            int rowValidate = Convert.ToInt32(stringNumber);

                            if (isNumber == false)
                            {
                                MessageBox.Show("ValueError");
                                return;
                            }

                            //TOMAR POSICION DE LA PRIMERA LETRA
                            char numeroLetra = Convert.ToChar(segundoValor);

                            int posicionLetra = char.ToUpper(numeroLetra) - 64;
                            var valor = "";

                            try
                            {
                                valor = matrizResult[rowValidate - 1, posicionLetra - 1];
                            }
                            catch
                            {
                                MessageBox.Show("ReferenceError");
                                return;
                            }

                            var valueOne = valor;

                            //VALIDAR  OPERACION

                            

                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        //VALIDAR SIN FUNCIONES

                    }
                }

            }
        }
         
    }

 
}
