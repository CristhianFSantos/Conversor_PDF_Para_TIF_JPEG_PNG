using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

namespace PdfToJpeg
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\temp";
            ConvertPdfToJpeg(path);
        }

        // DEPENDENCIA DO MÉTODO https://www.nuget.org/packages/PdfiumViewer/
        // • PdfiumViewer
        // • PdfiumViewer.Native.x86_64.v8-xfa
        static void ConvertPdfToJpeg(string path)
        {
            try
            {
                // CAPTURANDO O NOME DOS ARQUIVOS CONTIDOS NO DIRETÓRIO E ITERANDO SOBRE CADA UM DELES.
                IEnumerable<string> files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);
                foreach (string fileString in files)
                {
                    // INSTANCIANDO A CLASSE FILEINFO PARA VALIDAR A EXTENSÃO DO ARQUIVO.
                    FileInfo file = new(fileString);
                    if (file.Extension == ".pdf")
                    {
                        // INSTANCIANDO A CLASSE DA BIBLIOTECA QUE MANIPULA PDF.
                        using var pdf = PdfDocument.Load(file.ToString());
                        {
                            // CONTANDO A QUANTIDADE DE PÁGINAS DO ARQUIVO PDF E RENDERIZANDO UM JPEG PARA CADA PÁGINA.
                            int pageCount = pdf.PageCount;
                            for (int i = 0; i < pageCount; i++)
                            {
                                var image = pdf.Render(i, 300, 300, true);

                                // MONTANDO O NOME DO ARQUIVO, COM CONTADOR DE PAGINAS E REMOVENDO A EXTENSÃO ANTIGA.
                                string pathDestination = $@"{file.FullName}_page {i + 1}.jpeg".Replace($"{file.Extension}", "");
                                image.Save(pathDestination, ImageFormat.Jpeg);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
