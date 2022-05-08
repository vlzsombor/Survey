﻿using Microsoft.AspNetCore.Components;
using Survey.Shared.DTOs;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using Survey.Client.Util;

using System.IO;
namespace Survey.Client.Pages.App.Board
{
    public partial class Summary : ComponentBase
    {

        public List<(CardModel, Dictionary<int, int>)> ToShowList = new List<(CardModel, Dictionary<int, int>)>();


        [Inject]
        public Microsoft.JSInterop.IJSRuntime JS { get; set; } = null!;

        public void ExportToPdf()
        {
            int paragraphAfterSpacing = 8;
            int cellMargin = 8;
            PdfDocument pdfDocument = new PdfDocument();
            //Add Page to the PDF document.
            PdfPage page = pdfDocument.Pages.Add();

            //Create a new font.
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

            //Create a text element to draw a text in PDF page.
            PdfTextElement title = new PdfTextElement("PDF:", font, PdfBrushes.Black);
            PdfLayoutResult result = title.Draw(page, new PointF(0, 0));


            PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            PdfTextElement content = new PdfTextElement("This component demonstrates fetching data from a client side and Exporting the data to PDF document using Syncfusion .NET PDF library.", contentFont, PdfBrushes.Black);
            PdfLayoutFormat format = new PdfLayoutFormat();
            format.Layout = PdfLayoutType.Paginate;

            //Draw a text to the PDF document.
            result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Style.CellPadding.Left = cellMargin;
            pdfGrid.Style.CellPadding.Right = cellMargin;

            //Applying built-in style to the PDF grid
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable5DarkAccent2);

            string[] a = { "1", "2" };

            //Assign data source.
            pdfGrid.DataSource = CardList?.Select(x => x.CardModel)
                .Select(x => new
                {
                    x.Title,
                    x.Text,


                    _1 = x.Rating.Where(y=>y.RatingNumber==1)
                        .GroupBy(y=>y.RatingNumber)
                        .Count(),
                    _2 = x.Rating.Where(y=>y.RatingNumber==2)
                        .GroupBy(y=>y.RatingNumber)
                        .Count(),
                    _3 = x.Rating.Where(y=>y.RatingNumber==3)
                        .GroupBy(y=>y.RatingNumber)
                        .Count(),
                    _4 = x.Rating.Where(y=>y.RatingNumber==4)
                        .GroupBy(y=>y.RatingNumber)
                        .Count(),
                    _5 = x.Rating.Where(y=>y.RatingNumber==5)
                        .GroupBy(y=>y.RatingNumber)
                        .Count(),
                    _6 = x.Rating.Where(y=>y.RatingNumber==6)
                        .GroupBy(y=>y.RatingNumber)
                        .Count(),
                    _7 = x.Rating.Where(y=>y.RatingNumber==7)
                        .GroupBy(y=>y.RatingNumber)
                        .Count()
                    

                    
                });

            pdfGrid.Style.Font = contentFont;


            //Draw PDF grid into the PDF page.
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

            MemoryStream memoryStream = new MemoryStream();

            // Save the PDF document.
            pdfDocument.Save(memoryStream);



            // Download the PDF document
            JS.SaveAs("Sample.pdf", memoryStream.ToArray());
        }

    }

}
