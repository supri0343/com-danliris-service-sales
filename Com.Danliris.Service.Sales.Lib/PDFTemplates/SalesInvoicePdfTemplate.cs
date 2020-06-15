using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Com.Danliris.Service.Sales.Lib.PDFTemplates
{
    public class SalesInvoicePdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(SalesInvoiceViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN = 15;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font Title_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region customViewModel

            double result = 0;
            double totalTax = 0;
            double totalPay = 0;

            var currencyLocal = "";
            if (viewModel.Currency.Symbol == "Rp")
            {
                currencyLocal = "Rupiah";
            }
            else if (viewModel.Currency.Symbol == "$")
            {
                currencyLocal = "Dollar";
            }
            else
            {
                currencyLocal = viewModel.Currency.Symbol;
            }

            #endregion

            if (viewModel.SalesType == "Lokal")
            {
                #region Header

                PdfPTable headerTable = new PdfPTable(2);
                PdfPTable headerTable1 = new PdfPTable(1);
                PdfPTable headerTable2 = new PdfPTable(1);
                PdfPTable headerTable3 = new PdfPTable(2);
                PdfPTable headerTable4 = new PdfPTable(2);
                headerTable.SetWidths(new float[] { 10f, 10f });
                headerTable.WidthPercentage = 100;
                headerTable3.SetWidths(new float[] { 20f, 40f });
                headerTable3.WidthPercentage = 80;
                headerTable4.SetWidths(new float[] { 10f, 40f });
                headerTable4.WidthPercentage = 100;

                PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderBody2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderCS2 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER };

                cellHeaderBody.Phrase = new Phrase("PT. DAN LIRIS", Title_bold_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("Head Office : Jl. Merapi No. 23 Banaran, Grogol", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("Sukoharjo, 57552 Central Java, Indonesia", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("Telp  :(+62 271) 740888, 714400", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("Fax  :(+62 271) 740777, 735222", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("PO BOX 116 Solo, 57100", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("Web: www.danliris.com", normal_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeader1.AddElement(headerTable1);
                headerTable.AddCell(cellHeader1);

                cellHeaderBody2.HorizontalAlignment = Element.ALIGN_CENTER;

                cellHeaderBody2.Phrase = new Phrase("FM-PJ-00-03-007", bold_font);
                headerTable2.AddCell(cellHeaderBody2);
                cellHeaderBody2.Phrase = new Phrase("Sukoharjo, " + viewModel.SalesInvoiceDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
                headerTable2.AddCell(cellHeaderBody2);
                cellHeaderBody2.Phrase = new Phrase("" + viewModel.Buyer.Name, normal_font);
                headerTable2.AddCell(cellHeaderBody2);
                cellHeaderBody2.Phrase = new Phrase("" + viewModel.Buyer.Address, normal_font);
                headerTable2.AddCell(cellHeaderBody2);

                cellHeader2.AddElement(headerTable2);
                headerTable.AddCell(cellHeader2);

                cellHeaderCS2.Phrase = new Phrase("FAKTUR PENJUALAN", header_font);
                headerTable.AddCell(cellHeaderCS2);
                cellHeaderCS2.Phrase = new Phrase($"No. {viewModel.SalesInvoiceType}{viewModel.AutoIncreament.ToString().PadLeft(6, '0')}", bold_font);
                headerTable.AddCell(cellHeaderCS2);
                cellHeaderCS2.Phrase = new Phrase("", normal_font);
                headerTable.AddCell(cellHeaderCS2);


                cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

                cellHeaderBody.Phrase = new Phrase("NPWP ", normal_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(": 01.139.907.8.532.000", normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("NPPKP ", normal_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(": 01.139.907.8.532.000", normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("No Index Debitur ", normal_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(": " + viewModel.Buyer.Code, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeader3.AddElement(headerTable3);
                headerTable.AddCell(cellHeader3);


                cellHeaderBody.Phrase = new Phrase("NIK", normal_font);
                headerTable4.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(": " + viewModel.Buyer.NIK, normal_font);
                headerTable4.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("NPWP Buyer", normal_font);
                headerTable4.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(": " + viewModel.Buyer.NPWP, normal_font);
                headerTable4.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable4.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable4.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable4.AddCell(cellHeaderBody);

                cellHeader4.AddElement(headerTable4);
                headerTable.AddCell(cellHeader4);

                cellHeaderCS2.Phrase = new Phrase("", normal_font);
                headerTable.AddCell(cellHeaderCS2);

                document.Add(headerTable);

                #endregion Header

                #region Body

                PdfPTable bodyTable = new PdfPTable(7);
                PdfPCell bodyCell = new PdfPCell();

                float[] widthsBody = new float[] { 6f, 22f, 6f, 6f, 10f, 8f, 10f };
                bodyTable.SetWidths(widthsBody);
                bodyTable.WidthPercentage = 100;

                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;

                bodyCell.Phrase = new Phrase("Kode", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Nama Barang", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Banyak", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Jumlah", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Satuan", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Harga", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("QuantityItem", bold_font);
                bodyTable.AddCell(bodyCell);

                foreach (var detail in viewModel.SalesInvoiceDetails)
                {
                    foreach (var item in detail.SalesInvoiceItems)
                    {
                        bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        bodyCell.Phrase = new Phrase(item.ProductCode, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        bodyCell.Phrase = new Phrase(item.ProductName, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.Phrase = new Phrase(item.QuantityPacking + " " + item.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.Phrase = new Phrase(item.QuantityItem.GetValueOrDefault().ToString("N2"), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.Phrase = new Phrase(item.ItemUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.Phrase = new Phrase(item.Price.GetValueOrDefault().ToString("N2"), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.Phrase = new Phrase(item.Amount.GetValueOrDefault().ToString("N2"), normal_font);
                        bodyTable.AddCell(bodyCell);
                    }
                }

                foreach (var item in viewModel.SalesInvoiceDetails)
                {
                    foreach (var amount in item.SalesInvoiceItems)
                    {
                        result += amount.Amount.GetValueOrDefault();
                    }
                }
                totalTax = result * 0.1;
                totalPay = totalTax + result;

                document.Add(bodyTable);

                #endregion Body

                #region Footer

                var dueDate = viewModel.DueDate.Value.Date;
                var salesInvoiceDate = viewModel.SalesInvoiceDate.Value.Date;
                var tempo = (dueDate - salesInvoiceDate).ToString("dd");

                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;

                string TotalPayWithVat = textInfo.ToTitleCase(NumberToTextIDN.terbilang(totalPay));
                string TotalPayWithoutVat = textInfo.ToTitleCase(NumberToTextIDN.terbilang(result));

                //string TotalPayWithVat = NumberToTextIDN.terbilang(totalPay);
                //string TotalPayWithoutVat = NumberToTextIDN.terbilang(result);

                PdfPTable footerTable = new PdfPTable(2);
                PdfPTable footerTable1 = new PdfPTable(1);
                PdfPTable footerTable2 = new PdfPTable(2);
                PdfPTable footerTable3 = new PdfPTable(2);

                footerTable.SetWidths(new float[] { 10f, 10f });
                footerTable.WidthPercentage = 100;
                footerTable1.WidthPercentage = 100;
                footerTable2.SetWidths(new float[] { 10f, 50f });
                footerTable2.WidthPercentage = 100;
                footerTable3.SetWidths(new float[] { 30f, 50f });
                footerTable3.WidthPercentage = 100;

                PdfPCell cellFooterLeft1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellFooterLeft2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellFooterLeft3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderFooter = new PdfPCell() { Border = Rectangle.NO_BORDER };


                cellHeaderFooter.HorizontalAlignment = Element.ALIGN_LEFT;

                cellHeaderFooter.Phrase = new Phrase("", normal_font);
                footerTable2.AddCell(cellHeaderFooter);
                cellHeaderFooter.Phrase = new Phrase("", normal_font);
                footerTable2.AddCell(cellHeaderFooter);

                cellHeaderFooter.Phrase = new Phrase("Tempo", normal_font);
                footerTable2.AddCell(cellHeaderFooter);
                cellHeaderFooter.Phrase = new Phrase(": " + tempo + " Hari", normal_font);
                footerTable2.AddCell(cellHeaderFooter);

                cellHeaderFooter.Phrase = new Phrase("Jth. Tempo", normal_font);
                footerTable2.AddCell(cellHeaderFooter);
                cellHeaderFooter.Phrase = new Phrase(": " + viewModel.DueDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
                footerTable2.AddCell(cellHeaderFooter);

                cellHeaderFooter.Phrase = new Phrase("SJ No.", normal_font);
                footerTable2.AddCell(cellHeaderFooter);
                cellHeaderFooter.Phrase = new Phrase(": " + viewModel.DeliveryOrderNo, normal_font);
                footerTable2.AddCell(cellHeaderFooter);

                cellHeaderFooter.Phrase = new Phrase("", normal_font);
                footerTable2.AddCell(cellHeaderFooter);
                cellHeaderFooter.Phrase = new Phrase("", normal_font);
                footerTable2.AddCell(cellHeaderFooter);

                cellFooterLeft2.AddElement(footerTable2);
                footerTable.AddCell(cellFooterLeft2);

                cellHeaderFooter.Phrase = new Phrase("", normal_font);
                footerTable3.AddCell(cellHeaderFooter);
                cellHeaderFooter.Phrase = new Phrase("", normal_font);
                footerTable3.AddCell(cellHeaderFooter);

                cellHeaderFooter.Phrase = new Phrase("Dasar pengenaan pajak", normal_font);
                footerTable3.AddCell(cellHeaderFooter);
                cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + result.ToString("N2"), normal_font);
                footerTable3.AddCell(cellHeaderFooter);

                if (viewModel.VatType.Equals("PPN Umum"))
                {
                    cellHeaderFooter.Phrase = new Phrase("PPN 10%", normal_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + totalTax.ToString("N2"), normal_font);
                    footerTable3.AddCell(cellHeaderFooter);

                    cellHeaderFooter.Phrase = new Phrase("Jumlah", bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + totalPay.ToString("N2"), bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                }
                else if (viewModel.VatType.Equals("PPN Kawasan Berikat"))
                {

                    cellHeaderFooter.Phrase = new Phrase("PPN", normal_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": Tarif PPN 0% (Berfasilitas)", normal_font);
                    footerTable3.AddCell(cellHeaderFooter);

                    cellHeaderFooter.Phrase = new Phrase("Jumlah", bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + totalPay.ToString("N2"), bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                }
                else if (viewModel.VatType.Equals("PPN BUMN"))
                {
                    cellHeaderFooter.Phrase = new Phrase("PPN 10%", normal_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + totalTax.ToString("N2") + " (Dibayar terpisah)", normal_font);
                    footerTable3.AddCell(cellHeaderFooter);

                    cellHeaderFooter.Phrase = new Phrase("Jumlah", bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + result.ToString("N2"), bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                }
                else if (viewModel.VatType.Equals("PPN Retail"))
                {
                    cellHeaderFooter.Phrase = new Phrase("PPN 10%", normal_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + totalTax.ToString("N2"), normal_font);
                    footerTable3.AddCell(cellHeaderFooter);

                    cellHeaderFooter.Phrase = new Phrase("Jumlah", bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + totalPay.ToString("N2"), bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                }
                else
                {
                    cellHeaderFooter.Phrase = new Phrase("Jumlah", bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                    cellHeaderFooter.Phrase = new Phrase(": " + viewModel.Currency.Symbol + " " + result.ToString("N2"), bold_font);
                    footerTable3.AddCell(cellHeaderFooter);
                }

                cellHeaderFooter.Phrase = new Phrase("", normal_font);
                footerTable3.AddCell(cellHeaderFooter);
                cellHeaderFooter.Phrase = new Phrase("", normal_font);
                footerTable3.AddCell(cellHeaderFooter);

                cellFooterLeft3.AddElement(footerTable3);
                footerTable.AddCell(cellFooterLeft3);

                document.Add(footerTable);

                cellFooterLeft1.Phrase = new Phrase("", normal_font);
                footerTable1.AddCell(cellFooterLeft1);

                if (viewModel.VatType.Equals("PPN Umum"))
                {
                    cellFooterLeft1.Phrase = new Phrase("Terbilang : " + TotalPayWithVat + " " + currencyLocal, normal_font);
                    footerTable1.AddCell(cellFooterLeft1);
                }
                else if (viewModel.VatType.Equals("PPN Kawasan Berikat"))
                {
                    cellFooterLeft1.Phrase = new Phrase("Terbilang : " + TotalPayWithVat + " " + currencyLocal, normal_font);
                    footerTable1.AddCell(cellFooterLeft1);
                }
                else if (viewModel.VatType.Equals("PPN BUMN"))
                {
                    cellFooterLeft1.Phrase = new Phrase("Terbilang : " + TotalPayWithoutVat + " " + currencyLocal, normal_font);
                    footerTable1.AddCell(cellFooterLeft1);
                }
                else if (viewModel.VatType.Equals("PPN Retail"))
                {
                    cellFooterLeft1.Phrase = new Phrase("Terbilang : " + TotalPayWithVat + " " + currencyLocal, normal_font);
                    footerTable1.AddCell(cellFooterLeft1);
                }
                else
                {
                    cellFooterLeft1.Phrase = new Phrase("Terbilang : " + TotalPayWithoutVat + " " + currencyLocal, normal_font);
                    footerTable1.AddCell(cellFooterLeft1);
                }

                cellFooterLeft1.Phrase = new Phrase("", normal_font);
                footerTable1.AddCell(cellFooterLeft1);

                cellFooterLeft1.Phrase = new Phrase("Catatan : " + viewModel.Remark, bold_font);
                footerTable1.AddCell(cellFooterLeft1);

                cellFooterLeft1.Phrase = new Phrase("", normal_font);
                footerTable1.AddCell(cellFooterLeft1);
                cellFooterLeft1.Phrase = new Phrase("", normal_font);
                footerTable1.AddCell(cellFooterLeft1);
                cellFooterLeft1.Phrase = new Phrase("", normal_font);
                footerTable1.AddCell(cellFooterLeft1);

                PdfPTable signatureTable = new PdfPTable(4);
                PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };
                float[] widthsSignature = new float[] { 6f, 6f, 6f, 6f };
                signatureTable.SetWidths(widthsSignature);
                signatureTable.WidthPercentage = 30;

                signatureCell.Phrase = new Phrase("Tanda terima :", normal_font);
                signatureTable.AddCell(signatureCell);
                signatureCell.Phrase = new Phrase("Dibuat oleh :", normal_font);
                signatureTable.AddCell(signatureCell);
                signatureCell.Phrase = new Phrase("Diperiksa oleh :", normal_font);
                signatureTable.AddCell(signatureCell);
                signatureCell.Phrase = new Phrase("Disetujui oleh :", normal_font);
                signatureTable.AddCell(signatureCell);

                signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("---------------------------------", normal_font),
                    FixedHeight = 40,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                }); signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("---------------------------------", normal_font),
                    FixedHeight = 40,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                }); signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("---------------------------------", normal_font),
                    FixedHeight = 40,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                }); signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("---------------------------------", normal_font),
                    FixedHeight = 40,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                footerTable1.AddCell(new PdfPCell(signatureTable));

                cellFooterLeft1.Phrase = new Phrase("", normal_font);
                footerTable1.AddCell(cellFooterLeft1);
                document.Add(footerTable1);

                #endregion Footer
            }
            else if (viewModel.SalesType == "Ekspor")
            {
                #region Header

                #region Header_A
                PdfPTable headerTable_A = new PdfPTable(3);
                PdfPTable headerTable_A1 = new PdfPTable(1);
                PdfPTable headerTable_A2 = new PdfPTable(1);
                PdfPTable headerTable_A3 = new PdfPTable(1);
                headerTable_A.SetWidths(new float[] { 10f, 10f, 10f });
                headerTable_A.WidthPercentage = 100;

                PdfPCell cellHeaderBody_A = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader_A1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader_A2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader_A3 = new PdfPCell() { Border = Rectangle.NO_BORDER };

                cellHeaderBody_A.Phrase = new Phrase("\n", normal_font);
                headerTable_A1.AddCell(cellHeaderBody_A);

                cellHeaderBody_A.Phrase = new Phrase("INVOICE NO : " + viewModel.SalesInvoiceNo, normal_font);
                headerTable_A1.AddCell(cellHeaderBody_A);

                cellHeader_A1.AddElement(headerTable_A1);
                headerTable_A.AddCell(cellHeader_A1);

                cellHeaderBody_A.Phrase = new Phrase("\n", normal_font);
                headerTable_A2.AddCell(cellHeaderBody_A);

                cellHeaderBody_A.Phrase = new Phrase("DATE : " + viewModel.SalesInvoiceDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
                headerTable_A2.AddCell(cellHeaderBody_A);

                cellHeader_A2.AddElement(headerTable_A2);
                headerTable_A.AddCell(cellHeader_A2);

                cellHeaderBody_A.Phrase = new Phrase("FM-PJ-00-03-006", normal_font);
                headerTable_A3.AddCell(cellHeaderBody_A);

                cellHeaderBody_A.Phrase = new Phrase("Page : " + "?????" + " of " + "?????", normal_font);
                headerTable_A3.AddCell(cellHeaderBody_A);

                cellHeader_A3.AddElement(headerTable_A3);
                headerTable_A.AddCell(cellHeader_A3);

                document.Add(headerTable_A);
                #endregion Header_A

                #region Header_B
                PdfPTable headerTable_B = new PdfPTable(2);
                PdfPTable headerTable_B1 = new PdfPTable(1);
                PdfPTable headerTable_B2 = new PdfPTable(1);
                headerTable_B.SetWidths(new float[] { 10f, 10f });
                headerTable_B.WidthPercentage = 100;

                PdfPCell cellHeaderBody_B = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader_B1 = new PdfPCell();
                PdfPCell cellHeader_B2 = new PdfPCell();

                cellHeaderBody_B.Phrase = new Phrase("SOLD BY ORDERS AND FOR ACCOUNT AND RISK OF MESSRS : " + viewModel.Buyer.Name + "\n" + viewModel.Buyer.Address, normal_font);
                headerTable_B1.AddCell(cellHeaderBody_B);

                cellHeader_B1.AddElement(headerTable_B1);
                headerTable_B.AddCell(cellHeader_B1);

                cellHeaderBody_B.Phrase = new Phrase("CONTRACT NO : " + "?????", normal_font);
                headerTable_B2.AddCell(cellHeaderBody_B);

                cellHeaderBody_B.Phrase = new Phrase("SHIPPED PER : " + viewModel.ShippedPer, normal_font);
                headerTable_B2.AddCell(cellHeaderBody_B);

                cellHeaderBody_B.Phrase = new Phrase("SAILING ON OR ABOUT : " + viewModel.SailingDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
                headerTable_B2.AddCell(cellHeaderBody_B);

                cellHeaderBody_B.Phrase = new Phrase("FROM : SEMARANG, INDONESIA", normal_font);
                headerTable_B2.AddCell(cellHeaderBody_B);

                cellHeaderBody_B.Phrase = new Phrase("TO : " + viewModel.Buyer.Address, normal_font);
                headerTable_B2.AddCell(cellHeaderBody_B);

                cellHeader_B2.AddElement(headerTable_B2);
                headerTable_B.AddCell(cellHeader_B2);

                document.Add(headerTable_B);
                #endregion Header_B

                #region Header_C
                PdfPTable headerTable_C = new PdfPTable(2);
                PdfPTable headerTable_C1 = new PdfPTable(1);
                PdfPTable headerTable_C2 = new PdfPTable(1);
                headerTable_C.SetWidths(new float[] { 10f, 10f });
                headerTable_C.WidthPercentage = 100;

                PdfPCell cellHeaderBody_C = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader_C1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader_C2 = new PdfPCell() { Border = Rectangle.NO_BORDER };

                cellHeaderBody_C.Phrase = new Phrase("\n", normal_font);
                headerTable_C1.AddCell(cellHeaderBody_C);

                cellHeaderBody_C.Phrase = new Phrase("LETTER OF CREDIT NUMBER  : " + "?????", normal_font);
                headerTable_C1.AddCell(cellHeaderBody_C);

                cellHeaderBody_C.Phrase = new Phrase("ISSUED BY  : " + "?????", normal_font);
                headerTable_C1.AddCell(cellHeaderBody_C);

                cellHeader_C1.AddElement(headerTable_C1);
                headerTable_C.AddCell(cellHeader_C1);

                cellHeaderBody_C.Phrase = new Phrase("\n", normal_font);
                headerTable_C2.AddCell(cellHeaderBody_C);

                cellHeaderBody_C.Phrase = new Phrase("DATE : " + "?????", normal_font);
                headerTable_C2.AddCell(cellHeaderBody_C);

                cellHeaderBody_C.Phrase = new Phrase("", normal_font);
                headerTable_C1.AddCell(cellHeaderBody_C);

                cellHeader_C2.AddElement(headerTable_C2);
                headerTable_C.AddCell(cellHeader_C2);

                document.Add(headerTable_C);
                #endregion Header_C

                #endregion Header

                #region Body

                #region Body_A
                PdfPTable bodyTable_A = new PdfPTable(4);
                PdfPCell bodyCell_A = new PdfPCell();

                float[] widthsBody = new float[] { 20f, 8f, 8f, 10f };
                bodyTable_A.SetWidths(widthsBody);
                bodyTable_A.WidthPercentage = 100;

                bodyCell_A.HorizontalAlignment = Element.ALIGN_CENTER;

                bodyCell_A.Phrase = new Phrase("DESCRIPTION", bold_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase("QUANTITY IN METERS", bold_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase("UNIT PRICE USD", bold_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase("TOTAL PRICE USD", bold_font);
                bodyTable_A.AddCell(bodyCell_A);

                double totalPrice = 0;
                double grandTotalPrice = 0;
                double totalLength = 0;

                foreach (var detail in viewModel.SalesInvoiceDetails)
                {
                    foreach (var item in detail.SalesInvoiceItems)
                    {
                        totalPrice = item.QuantityItem.GetValueOrDefault() * item.Price.GetValueOrDefault();
                        grandTotalPrice += totalPrice;
                        totalLength += item.QuantityItem.GetValueOrDefault();

                        bodyCell_A.HorizontalAlignment = Element.ALIGN_LEFT;
                        bodyCell_A.Phrase = new Phrase(item.ProductName, normal_font);
                        bodyTable_A.AddCell(bodyCell_A);

                        bodyCell_A.HorizontalAlignment = Element.ALIGN_LEFT;
                        bodyCell_A.Phrase = new Phrase(string.Format("{0:n2}", item.QuantityItem), normal_font);
                        bodyTable_A.AddCell(bodyCell_A);

                        bodyCell_A.HorizontalAlignment = Element.ALIGN_LEFT;
                        bodyCell_A.Phrase = new Phrase(string.Format("{0:n0}", item.Price), normal_font);
                        bodyTable_A.AddCell(bodyCell_A);

                        bodyCell_A.HorizontalAlignment = Element.ALIGN_LEFT;
                        bodyCell_A.Phrase = new Phrase(totalPrice.ToString("N2"), normal_font);
                        bodyTable_A.AddCell(bodyCell_A);
                    }
                }


                bodyCell_A.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell_A.Phrase = new Phrase("TOTAL", bold_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase(totalLength.ToString("N2"), bold_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase(".............", bold_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase(grandTotalPrice.ToString("N2"), bold_font);
                bodyTable_A.AddCell(bodyCell_A);

                document.Add(bodyTable_A);
                #endregion Body_A

                #region Body_B
                PdfPTable bodyTable_B = new PdfPTable(2);
                PdfPTable bodyTable_B1 = new PdfPTable(1);
                PdfPTable bodyTable_B2 = new PdfPTable(1);
                bodyTable_B.SetWidths(new float[] { 10f, 10f });
                bodyTable_B.WidthPercentage = 100;

                PdfPCell cellBody_B = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell bodyCell_A1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell bodyCell_A2 = new PdfPCell() { Border = Rectangle.NO_BORDER };

                cellBody_B.Phrase = new Phrase("", normal_font);
                bodyTable_B1.AddCell(cellBody_B);

                cellBody_B.Phrase = new Phrase("SAY : " + "?????", normal_font);
                bodyTable_B1.AddCell(cellBody_B);

                cellBody_B.Phrase = new Phrase("SHIPPING MARKS : " + "?????", normal_font);
                bodyTable_B1.AddCell(cellBody_B);

                bodyCell_A1.AddElement(bodyTable_B1);
                bodyTable_B.AddCell(bodyCell_A1);

                cellBody_B.Phrase = new Phrase("", normal_font);
                bodyTable_B2.AddCell(cellBody_B);

                cellBody_B.Phrase = new Phrase("FOR SEMARANG", normal_font);
                bodyTable_B2.AddCell(cellBody_B);

                cellBody_B.Phrase = new Phrase("REMARKS : " + "?????", normal_font);
                bodyTable_B2.AddCell(cellBody_B);

                bodyCell_A2.AddElement(bodyTable_B2);
                bodyTable_B.AddCell(bodyCell_A2);

                document.Add(bodyTable_B);
                #endregion Body_B

                #endregion Body

                #region Footer

                #region Footer_A
                PdfPTable footerTable_A = new PdfPTable(1);
                PdfPTable footerTable_A1 = new PdfPTable(2);
                footerTable_A1.SetWidths(new float[] { 10f, 40f });
                footerTable_A1.WidthPercentage = 80;

                PdfPCell cellFooter_A = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell footerCell_A1 = new PdfPCell() { Border = Rectangle.NO_BORDER };

                footerTable_A.HorizontalAlignment = Element.ALIGN_LEFT;
                cellFooter_A.HorizontalAlignment = Element.ALIGN_LEFT;

                cellFooter_A.Phrase = new Phrase("", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase("", normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("COLOR", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + viewModel.Color, normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("ORDER NO.", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + viewModel.OrderNo, normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("INDENT", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + viewModel.Indent, normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("QUANTITY", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + viewModel.QuantityLength + " " + viewModel.PaymentType, normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("CARTON NO.", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + viewModel.CartonNo, normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase("", normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("GROSS WEIGHT", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + viewModel.GrossWeight + " " + viewModel.WeightUom + "S", normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("NETT WEIGHT" , normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + viewModel.NetWeight + " " + viewModel.WeightUom + "S", normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("TOTAL MEASS.", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + viewModel.TotalMeas + " " + viewModel.TotalUom, normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                footerCell_A1.AddElement(footerTable_A1);
                footerTable_A.AddCell(footerCell_A1);

                document.Add(footerTable_A);
                #endregion Footer_A

                #region Footer_B
                PdfPTable footerTable_B = new PdfPTable(1);
                PdfPTable footerTable_B1 = new PdfPTable(1);

                PdfPCell cellFooter_B = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell footerCell_B1 = new PdfPCell() { Border = Rectangle.NO_BORDER };

                footerTable_B.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellFooter_B.HorizontalAlignment = Element.ALIGN_RIGHT;

                cellFooter_B.Phrase = new Phrase(" ( "+ viewModel.Sales + " ) \nAUTHORIZED SIGNATURE", normal_font);
                footerTable_B1.AddCell(cellFooter_B);

                //cellFooter_B.Phrase = new Phrase("AUTHORIZED SIGNATURE", normal_font);
                //footerTable_B1.AddCell(cellFooter_B);

                footerCell_B1.AddElement(footerTable_B1);
                footerTable_B.AddCell(footerCell_B1);

                document.Add(footerTable_B);
                #endregion Footer_B

                #endregion Footer
            }

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
