using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;

class Program
{
    static void Main()
    {
        // Prompt user for the folder path
        Console.WriteLine("Please enter the full path to the folder containing PDF files:");
        string folderPath = Console.ReadLine();

        try
        {
            // Get all PDF files in the folder
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

            if (pdfFiles.Length == 0)
            {
                Console.WriteLine("No PDF files found in the specified folder.");
                return;
            }

            // List all PDF files
            Console.WriteLine("Found the following PDF files:");
            for (int i = 0; i < pdfFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(pdfFiles[i])}");
            }

            // Prompt user to select a PDF file
            Console.WriteLine("Please enter the number of the PDF file you want to process:");
            int fileIndex = int.Parse(Console.ReadLine()) - 1;

            if (fileIndex < 0 || fileIndex >= pdfFiles.Length)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            string selectedPdfPath = pdfFiles[fileIndex];

            // Load the selected PDF document for reading
            using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(selectedPdfPath)))
            {
                // Accessing the AcroForm fields
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, false);
                if (form != null && form.GetAllFormFields().Count > 0)
                {
                    IDictionary<string, PdfFormField> fields = form.GetAllFormFields();

                    // List all form fields
                    Console.WriteLine($"Form fields in {Path.GetFileName(selectedPdfPath)}:");
                    foreach (var field in fields)
                    {
                        Console.WriteLine("Field name: " + field.Key);
                    }

                    Console.WriteLine("Form fields listed successfully.");
                }
                else
                {
                    Console.WriteLine("No AcroForm found in the PDF or no form fields available.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        // Keep the console open
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}
