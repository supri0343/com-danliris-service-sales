using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Sales.Lib.PDFTemplates
{
    public class DOSalesPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(DOSalesViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN = 15;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A5.Rotate(), MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            if (viewModel.DOSalesType == "Lokal")
            {
                #region Header

                PdfPTable headerTable = new PdfPTable(2);
                headerTable.SetWidths(new float[] { 10f, 10f });
                headerTable.WidthPercentage = 100;
                PdfPTable headerTable1 = new PdfPTable(1);
                PdfPTable headerTable2 = new PdfPTable(1);

                PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderCS2 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 2 };

                cellHeaderBody.Phrase = new Phrase("PT. DANLIRIS", header_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("No. " + viewModel.DOSalesNo, bold_font);
                headerTable1.AddCell(cellHeaderBody);
                //cellHeaderBody.Phrase = new Phrase("FM-PJ-00-03-005 / R2", bold_font);
                //headerTable1.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Harap dikeluarkan barang tersebut di bawah ini : ", normal_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeader1.AddElement(headerTable1);
                headerTable.AddCell(cellHeader1);

                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("Sukoharjo, " + viewModel.LocalDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("Kepada", normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("Yth. Bpk./Ibu. " + viewModel.LocalHeadOfStorage, normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("Bag. Gudang Packing Finishing/Printing", normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("D.O. PENJUALAN", bold_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                //cellHeaderBody.Phrase = new Phrase("Order dari " + viewModel.LocalBuyer.Name, normal_font);
                cellHeaderBody.Phrase = new Phrase("Order dari " + viewModel.DestinationBuyerName, normal_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeader2.AddElement(headerTable2);
                headerTable.AddCell(cellHeader2);

                cellHeaderCS2.Phrase = new Phrase("", normal_font);
                headerTable.AddCell(cellHeaderCS2);

                document.Add(headerTable);

                #endregion Header

                #region Custom
                int index = 1;
                double totalPackingQuantity = 0;
                double totalImperialQuantity = 0;
                double totalMetricQuantity = 0;
                #endregion

                #region Body

                PdfPTable bodyTable = new PdfPTable(7);
                PdfPCell bodyCell = new PdfPCell();

                float[] widthsBody = new float[] { 3f, 10f, 10f, 7f, 7f, 7f, 7f };
                bodyTable.SetWidths(widthsBody);
                bodyTable.WidthPercentage = 100;

                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase("No.", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("No. SPP", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Material Konstruksi", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Jenis / Code", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Total Packing\n(" + viewModel.PackingUom + ")", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Total Metrik\n(" + viewModel.MetricUom + ")", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase("Total Imperial\n(" + viewModel.ImperialUom + ")", bold_font);
                bodyTable.AddCell(bodyCell);

                foreach (DOSalesLocalViewModel item in viewModel.DOSalesLocalItems)
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.VerticalAlignment = Element.ALIGN_TOP;
                    bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyCell.Phrase = new Phrase(item.ProductionOrder.OrderNo, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyCell.Phrase = new Phrase(item.ProductionOrder.MaterialConstruction.Name, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyCell.Phrase = new Phrase(item.UnitOrCode, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    bodyCell.Phrase = new Phrase(string.Format("{0:n2}", item.TotalPacking), normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    bodyCell.Phrase = new Phrase(string.Format("{0:n2}", item.TotalMetric), normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    bodyCell.Phrase = new Phrase(string.Format("{0:n2}", item.TotalImperial), normal_font);
                    bodyTable.AddCell(bodyCell);
                }

                foreach (DOSalesLocalViewModel total in viewModel.DOSalesLocalItems)
                {
                    totalPackingQuantity += total.TotalPacking;
                    totalImperialQuantity += total.TotalImperial;
                    totalMetricQuantity += total.TotalMetric;
                }


                bodyCell.Colspan = 2;
                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Colspan = 1;
                bodyCell.Border = Rectangle.BOX;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase("Total", bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalPackingQuantity), bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalImperialQuantity), bold_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalMetricQuantity), bold_font);
                bodyTable.AddCell(bodyCell);

                document.Add(bodyTable);

                #endregion Body

                #region Footer

                PdfPTable footerTable = new PdfPTable(1);
                PdfPCell cellFooterLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

                float[] widthsFooter = new float[] { 10f };
                footerTable.SetWidths(widthsFooter);
                footerTable.WidthPercentage = 100;

                cellFooterLeft.Phrase = new Phrase("", normal_font);
                footerTable.AddCell(cellFooterLeft);
                cellFooterLeft.Phrase = new Phrase("", normal_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Phrase = new Phrase("Disp : " + viewModel?.Disp + "       Op : " + viewModel?.Op + "       Sc : " + viewModel?.Sc, normal_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Colspan = 3;
                cellFooterLeft.Phrase = new Phrase("", bold_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Colspan = 3;
                cellFooterLeft.Phrase = new Phrase("Dikirim Kepada : " + viewModel.DestinationBuyerName, bold_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Colspan = 3;
                cellFooterLeft.Phrase = new Phrase("Keterangan : " + viewModel.LocalRemark, bold_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Colspan = 3;
                cellFooterLeft.Phrase = new Phrase("", bold_font);
                footerTable.AddCell(cellFooterLeft);

                PdfPTable signatureTable = new PdfPTable(3) { HorizontalAlignment = Element.ALIGN_CENTER };
                PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };

                float[] widthsSignanture = new float[] { 10f, 10f, 10f };
                signatureTable.SetWidths(widthsSignanture);
                signatureTable.WidthPercentage = 100;

                signatureCell.Phrase = new Phrase("Adm.Penjualan", normal_font);
                signatureTable.AddCell(signatureCell);
                signatureCell.Phrase = new Phrase("Gudang", normal_font);
                signatureTable.AddCell(signatureCell);
                signatureCell.Phrase = new Phrase("Terima kasih :\nBagian Penjualan", normal_font);
                signatureTable.AddCell(signatureCell);

                signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("--------------------------------", normal_font),
                    FixedHeight = 40,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                }); signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("--------------------------------", normal_font),
                    FixedHeight = 40,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                }); signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("--------------------------------", normal_font),
                    FixedHeight = 40,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                footerTable.AddCell(new PdfPCell(signatureTable));

                cellFooterLeft.Phrase = new Phrase("", normal_font);
                footerTable.AddCell(cellFooterLeft);
                document.Add(footerTable);

                #endregion Footer

            } else if(viewModel.DOSalesType == "Ekspor")
            {
                #region Header
                PdfPTable headerTable_A = new PdfPTable(2);
                PdfPTable headerTable_B = new PdfPTable(1);
                PdfPTable headerTable1 = new PdfPTable(1);
                PdfPTable headerTable2 = new PdfPTable(1);
                PdfPTable headerTable3 = new PdfPTable(3);
                PdfPTable headerTable4 = new PdfPTable(2);
                headerTable_A.SetWidths(new float[] { 10f, 10f });
                headerTable_A.WidthPercentage = 100;
                headerTable3.SetWidths(new float[] { 40f, 4f, 100f });
                headerTable3.WidthPercentage = 100;
                headerTable4.SetWidths(new float[] { 10f, 40f });
                headerTable4.WidthPercentage = 100;

                PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };

                cellHeaderBody.Phrase = new Phrase("PT. DANLIRIS", bold_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("INDUSTRIAL & TRADING CO.LTD.", bold_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("Kel. Banaran (Selatan Lawehan)", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("Telp. 714400, 719113", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("SOLO - INDONESIA 57100", normal_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeader1.AddElement(headerTable1);
                headerTable_A.AddCell(cellHeader1);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;

                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("KERTAS KERJA DO. EKSPORT", header_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeader2.AddElement(headerTable2);
                headerTable_A.AddCell(cellHeader2);

                document.Add(headerTable_A);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

                cellHeaderBody.Phrase = new Phrase("No. DO Penjualan ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.DOSalesNo, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Tanggal ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.ExportDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);

                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Dikerjakan Oleh ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.DoneBy, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("No. Sales Contract ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.ExportSalesContract.SalesContractNo, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Order Untuk ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.ExportSalesContract.MaterialConstruction.Name, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Buyer ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.ExportSalesContract.Buyer.Name, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Panjang Satuan ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.ExportSalesContract.PieceLength, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Cap Komposisi Persen ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.ExportSalesContract.Commodity.Name, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Jumlah Order ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.ExportSalesContract.OrderQuantity.GetValueOrDefault().ToString("N2"), normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Isi tiap Bale", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.FillEachBale.GetValueOrDefault().ToString("N2"), normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Catatan ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.ExportRemark, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeader3.AddElement(headerTable3);
                headerTable_B.AddCell(cellHeader3);

                cellHeader4.AddElement(headerTable4);
                headerTable_B.AddCell(cellHeader4);

                document.Add(headerTable_B);

                #endregion Header
            }

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
