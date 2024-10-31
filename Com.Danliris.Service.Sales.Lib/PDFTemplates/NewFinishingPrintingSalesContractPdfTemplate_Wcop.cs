using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Sales.Lib.PDFTemplates
{
    public class NewFinishingPrintingSalesContractPdfTemplate_Wcop
    {
        public MemoryStream GeneratedPdfTemplate(FinishingPrintingSalesContractViewModel viewModel, /*FinishingPrintingSalesContractDetailViewModel detail,*/ int timeoffset)
        {
            
            //set content configuration
            //Font company_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            //Font title_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font remark_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font normal_font1 = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 5);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            //set pdf stream
            MemoryStream stream = new MemoryStream();
            Document document = new Document(PageSize.A4, 50, 50, 120, 20);
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new FPSalesContractWithHeaderPDFTemplatePageEvent(viewModel, timeoffset);
            document.Open();

            #region CustomViewModel
            string jumlahTerbilang = NumberToTextIDN.terbilang(viewModel.OrderQuantity.GetValueOrDefault());
            string jumlahHargaTerbilang = NumberToTextIDN.terbilang(viewModel.Amount.GetValueOrDefault());
            string tanggalClaimTerbilang = NumberToTextIDN.terbilang(viewModel.Claim.GetValueOrDefault());

            //var amount = viewModel.Details.Price * viewModel.OrderQuantity;

            var uom = "";
            var uom1 = "";
            //if (viewModel.Uom.Unit.ToLower() == "yds")
            if (viewModel.UOM.Unit.ToLower() == "yds")
            {
                uom = "YARD";
                uom1 = "YARDS";
            }
            else if (viewModel.UOM.Unit.ToLower() == "mtr")
            {
                uom = "METER";
                uom1 = "METERS";
            }
            else
            {
                uom = viewModel.UOM.Unit;
                uom = viewModel.UOM.Unit;
            }

            //var ppn = viewModel.IncomeTax;
            //if (ppn == "Include PPn")
            //{
            //    ppn = "Include PPn 11%";
            //}

            List<string> details = new List<string>();


            foreach (var i in viewModel.Details)
            {
                var nominal = string.Format("{0:n0}", i.Price);

                if (i.Currency.Code.ToLower() == "usd")
                {
                    nominal = string.Format("{0:n0}", i.Price);
                }
                //var color = i.Color;

                details.Add( nominal );
                
            }

            //foreach (var i in viewModel.Details)
            //{
            //    var amount = string.Format("{0:n2", i.Price * viewModel.OrderQuantity);
            //    details.Add(amount);
            //}

            #endregion

            #region Header
            string codeNoString = "FM-PJ-00-03-003/R2";
            Paragraph codeNo = new Paragraph(codeNoString, bold_font) { Alignment = Element.ALIGN_RIGHT };
            document.Add(codeNo);

            string titleString = "KONTRAK PENJUALAN";
            bold_font.SetStyle(Font.UNDERLINE);
            Paragraph title = new Paragraph(titleString, bold_font) { Alignment = Element.ALIGN_CENTER };
            // title.SpacingAfter = 20f;
            document.Add(title);
            bold_font.SetStyle(Font.NORMAL);

            //string codeNoString = "FM-00-PJ-02-001/R1";
            Paragraph scNo = new Paragraph(viewModel.SalesContractNo, bold_font) { Alignment = Element.ALIGN_CENTER };
            scNo.SpacingAfter = 10f;
            document.Add(scNo);
            #endregion

            string HeaderParagraphString = "Yang bertanda tangan dibawah ini : ";
            Paragraph HeaderParagraph = new Paragraph(HeaderParagraphString, normal_font) { Alignment = Element.ALIGN_LEFT };
            HeaderParagraph.SpacingAfter = 10f;
            document.Add(HeaderParagraph);

            #region Body
            PdfPTable tableBody = new PdfPTable(3);
            tableBody.SetWidths(new float[] { 0.004f, 0.009f, 0.060f });
            PdfPCell bodyContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 1, HorizontalAlignment = Element.ALIGN_LEFT };
            //PdfPCell bodyJustify = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_JUSTIFIED };
            bodyContentLeft.Phrase = new Phrase("1.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Nama", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": Robby Oentoro ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Jabatan", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": General Manager Penjualan", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Alamat", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": PT. DAN LIRIS JL. MERAPI NO. 23 BANARAN, GROGOL, SUKOHARJO, 57552, CENTRAL JAVA – INDONESIA ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            PdfPCell cellBody = new PdfPCell(tableBody); // dont remove
            tableBody.ExtendLastRow = false;
            tableBody.SpacingAfter = 0.5f;
            document.Add(tableBody);

            string ParagraphString1 = "          Bertindak untuk dan atas nama PT. Dan Liris, selanjutnya disebut Penjual.";
            Paragraph Paragraph1 = new Paragraph(ParagraphString1, normal_font) { Alignment = Element.ALIGN_LEFT };
            Paragraph1.SpacingAfter = 10f;
            document.Add(Paragraph1);

            PdfPTable tableBodyBuyer = new PdfPTable(3);
            tableBodyBuyer.SetWidths(new float[] { 0.004f, 0.010f, 0.060f });
            PdfPCell bodyContentLefts = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 1, HorizontalAlignment = Element.ALIGN_LEFT };
            bodyContentLefts.Phrase = new Phrase("2.", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("Nama" , normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase(": " + "" + UppercaseWords(viewModel.Buyer.BuyerOwner), normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("NIK " , normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase(": " + " " +  viewModel.Buyer.NIK, normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            //bodyContentLefts.Phrase = new Phrase("", normal_font);
            //tableBodyBuyer.AddCell(bodyContentLefts);
            //bodyContentLefts.Phrase = new Phrase("NPWP ", normal_font);
            //tableBodyBuyer.AddCell(bodyContentLefts);
            //bodyContentLefts.Phrase = new Phrase(": " + " " + viewModel.Buyer.NPWP, normal_font);
            //tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("Jabatan " , normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase(":"  + " " + viewModel.Buyer.Job, normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("Alamat", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            var addressBuyer = viewModel.Buyer.Address.Replace("\n", " ");
            bodyContentLefts.Phrase = new Phrase(":" + " " + UppercaseWords(addressBuyer), normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            PdfPCell cellBodys = new PdfPCell(tableBodyBuyer); // dont remove
            tableBodyBuyer.ExtendLastRow = false;
            tableBodyBuyer.SpacingAfter = 0.05f;
            document.Add(tableBodyBuyer);

            string ParagraphStringbuyer = "          Bertindak untuk dan atas nama " + "" + UppercaseWords(viewModel.Buyer.Name) + "" + ", selanjutnya disebut pembeli";
            Paragraph Paragraphbuyer = new Paragraph(ParagraphStringbuyer, normal_font) { Alignment = Element.ALIGN_LEFT };
            Paragraphbuyer.SpacingAfter = 10f;
            document.Add(Paragraphbuyer);

            string ParagraphString2 = "Secara bersama-sama Penjual dan Pembeli disebut Para Pihak ";
            Paragraph Paragraph2 = new Paragraph(ParagraphString2, normal_font) { Alignment = Element.ALIGN_LEFT };
            Paragraph2.SpacingAfter = 10f;
            document.Add(Paragraph2);

            string FirstParagraphString = "Para Pihak tersebut di atas sepakat mengadakan perjanjian jual beli produk yang diproduksi oleh Penjual, dengan spesifikasi dan syarat-syarat sebagai berikut: ";
            Paragraph FirstParagraph = new Paragraph(FirstParagraphString, normal_font) { Alignment = Element.ALIGN_LEFT };
            FirstParagraph.SpacingAfter = 10f;
            document.Add(FirstParagraph);

            string ParagraphString3 = "A. Produk Yang Diorder";
            Paragraph Paragraph3 = new Paragraph(ParagraphString3, bold_font) { Alignment = Element.ALIGN_LEFT };
            Paragraph3.SpacingAfter = 4f;
            document.Add(Paragraph3);

            //#region Produk diorder
            PdfPTable tableOrder = new PdfPTable(3);
            //tableOrder.TotalWidth = 400f;
            //tableOrder.LockedWidth = true;
            tableOrder.WidthPercentage = 100;
            float[] widths = new float[] { 5f, 7.5f, 6f };
            tableOrder.SetWidths(widths);
            tableOrder.HorizontalAlignment = 0;
            tableOrder.SpacingAfter = 20f;
            PdfPCell cellOrder = new PdfPCell() { MinimumHeight = 10, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            
            cellOrder.Phrase = new Phrase("Jenis Produk", bold_font);
            tableOrder.AddCell(cellOrder);
            cellOrder.Phrase = new Phrase("Material/Konstruksi", bold_font);
            tableOrder.AddCell(cellOrder);
            cellOrder.Phrase = new Phrase("Keterangan", bold_font);
            tableOrder.AddCell(cellOrder);
            cellOrder.Phrase = new Phrase(UppercaseWords(viewModel.OrderType.Name), normal_font);
            tableOrder.AddCell(cellOrder);
            cellOrder.Phrase = new Phrase(UppercaseWords(viewModel.Material.Name) + " " + " " + viewModel.MaterialConstruction.Name + "" + "/" + viewModel.YarnMaterial.Name +" - " + viewModel.MaterialWidth, normal_font);
            tableOrder.AddCell(cellOrder);
            cellOrder.Phrase = new Phrase(UppercaseWords(viewModel.Description), normal_font);
            tableOrder.AddCell(cellOrder);
            tableOrder.AddCell(cellOrder);



            PdfPCell cellProduct = new PdfPCell(tableOrder); // dont remove
            tableOrder.ExtendLastRow = false;
            tableOrder.SpacingAfter = 10f;
            Paragraph p = new Paragraph();
            p.IndentationLeft = 15f;
            p.Add(tableOrder);
            document.Add(p);

            //cellOrder.VerticalAlignment = Element.ALIGN_TOP;
            //tableOrder.AddCell(cellOrder);

            //tableOrder.SpacingAfter = 10;
            //document.Add(tableOrder);
            //#endregion

            string ParagraphString4 = "B. Kesepakatan Order";
            Paragraph Paragraph4 = new Paragraph(ParagraphString4, bold_font) { Alignment = Element.ALIGN_LEFT };
            Paragraph4.SpacingAfter = 4f;
            document.Add(Paragraph4);


            //#region Pemenuhan Order
            PdfPTable tableDetailOrder = new PdfPTable(2);
            //tableDetailOrder.WidthPercentage = 20;
            //tableDetailOrder.SetWidths(new float[] { 20f, 20f });
            //tableDetailOrder.TotalWidth = 270f;
            tableDetailOrder.WidthPercentage = 100;
            //tableDetailOrder.LockedWidth = true;
            float[] widthsDetail = new float[] { 1f, 2f };
            tableDetailOrder.SetWidths(widthsDetail);
            tableDetailOrder.HorizontalAlignment = 0;
            PdfPCell cellDetailOrder = new PdfPCell() { MinimumHeight = 10, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_MIDDLE };
            PdfPCell CellDetailCenter = new PdfPCell() { MinimumHeight = 10, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            cellDetailOrder.Phrase = new Phrase("Jumlah", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(viewModel.OrderQuantity.GetValueOrDefault().ToString() + " " + UppercaseWords(uom), normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Harga", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            int index = 0;
            var currency = viewModel.AccountBank.Currency.Code == "IDR" ? "Rp. " : viewModel.AccountBank.Currency.Code;
            foreach (var detail in viewModel.Details)
            {
                var nominal = string.Format("{0:n}", detail.Price);

                //if (detail.Currency.Code.ToLower() == "usd")
                //{
                //    nominal = string.Format("{0:n0}", detail.Price);
                //}
               
                index++;
                if (index == 1)
                {
                    CellDetailCenter.Phrase = new Phrase(UppercaseWords(detail.Color) +" - "+/*viewModel.AccountBank.Currency.Code*/ currency + " " + nominal, normal_font);
                    tableDetailOrder.AddCell(CellDetailCenter);
                }
                else
                {
                    CellDetailCenter.Phrase = new Phrase(" ", normal_font);
                    tableDetailOrder.AddCell(CellDetailCenter);
                    CellDetailCenter.Phrase = new Phrase(UppercaseWords(detail.Color) + " - " + /*viewModel.AccountBank.Currency.Code*/ currency + "  " + nominal, normal_font);
                    tableDetailOrder.AddCell(CellDetailCenter);
                }
            }
            //var date = viewModel.DeliverySchedule.Value.ToString("dd MMM yyyy", new CultureInfo("id-ID"));
            cellDetailOrder.Phrase = new Phrase("Total Harga", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            //cellDetailOrder.Phrase = new Phrase(Convert.ToString(viewModel.Amount), normal_font);
            CellDetailCenter.Phrase = new Phrase(currency + " " + string.Format("{0:n}", viewModel.Amount), normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Jenis Packing", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(UppercaseWords(viewModel.Packing), normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Jadwal Pengiriman", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(viewModel.DeliverySchedule.Value.ToString("dd MMM yyyy", new CultureInfo("id-ID")), normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Ongkos Angkut", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(UppercaseWords(viewModel.TransportFee), normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Alamat Pengiriman", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(UppercaseWords(viewModel.DeliveredTo), normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            //CheckBox checkBox1 = new CheckBox(20, 20, 15, 15, "checkBox1");
            //page.Annotations.Add(checkBox1);


            PdfPCell cellDetail = new PdfPCell(tableDetailOrder); // dont remove
            tableDetailOrder.ExtendLastRow = false;
            tableDetailOrder.SpacingAfter = 10f;
            Paragraph p1 = new Paragraph();
            p1.IndentationLeft=15f;
            p1.Add(tableDetailOrder);
            document.Add(p1);

            cellDetailOrder.VerticalAlignment = Element.ALIGN_TOP;
            tableDetailOrder.AddCell(cellDetailOrder);

            //cellDetailOrder.VerticalAlignment = Element.ALIGN_TOP;
            //tableDetailOrder.AddCell(cellDetailOrder);

            //tableDetailOrder.SpacingAfter = 10;
            //document.Add(tableDetailOrder);

            string ParagraphString5 = "C. Metode Pembayaran";
            Paragraph Paragraph5 = new Paragraph(ParagraphString5, bold_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(Paragraph5);

            Paragraph p2 = new Paragraph();
            PdfPTable tablePembayaran = new PdfPTable(4);
            tablePembayaran.SetWidths(new float[] { 0.3f, 3f, 0.2f, 5f });
            PdfPCell bodyContentPembayaran = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            //tablePembayaran.WidthPercentage = 100;
            bodyContentPembayaran.Phrase = new Phrase("1.", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase("Cara Pembayaran", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(":", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(UppercaseWords(viewModel.TermOfPayment.Name), normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);

            bodyContentPembayaran.Phrase = new Phrase("2.", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase("Down Payment (DP)", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(":", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(viewModel.DownPayments, normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);

            bodyContentPembayaran.Phrase = new Phrase("3.", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase("Down Payment (%)", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(":", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(viewModel.precentageDP + " %", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);

            bodyContentPembayaran.Phrase = new Phrase("3.", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase("Rekening Tujuan Pembayaran", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(":", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(UppercaseWords(viewModel.AccountBank.AccountName) + " - " +viewModel.AccountBank.BankName +" - "+ viewModel.AccountBank.AccountNumber, normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);

            bodyContentPembayaran.Phrase = new Phrase("4.", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase("Pembayaran dianggap sah / lunas jika diterima penjual sesusai dengan nilai tagihan.", normal_font);
            bodyContentPembayaran.Colspan = 3;
            tablePembayaran.AddCell(bodyContentPembayaran);
            //bodyContentPembayaran.Phrase = new Phrase("", normal_font);
            //tablePembayaran.AddCell(bodyContentPembayaran);
            //bodyContentPembayaran.Phrase = new Phrase("", normal_font);
            //tablePembayaran.AddCell(bodyContentPembayaran);
            PdfPCell cellPembayaran = new PdfPCell(tablePembayaran); // dont remove
            tablePembayaran.ExtendLastRow = false;
            tablePembayaran.SpacingAfter = 0.5f;
            p2.IndentationLeft = 15f;
            p2.Add(tablePembayaran);
            document.Add(p2);
            //document.Add(tablePembayaran);
            //string ParagraphStringPembayaran = "4. Pembayaran dianggap sah / lunas jika diterima penjual sesusai dengan nilai tagihan.";
            //Paragraph ParagraphPembayaran = new Paragraph(ParagraphStringPembayaran, normal_font) { Alignment = Element.ALIGN_LEFT };
            //tablePembayaran.SpacingAfter = 30f;
            //document.Add(ParagraphPembayaran);


            #endregion

            #region Signatures
            PdfPTable tableSignature = new PdfPTable(2);
            //tableSignature.WidthPercentage = 20;
            //tableSignature.SetWidths(new int[] { 10, 10 });
            tableSignature.TotalWidth = 216f;
            tableSignature.LockedWidth = true;
            float[] widthsSignature = new float[] { 1f, 1f };
            tableSignature.SetWidths(widthsSignature);
            tableSignature.HorizontalAlignment = 0;
            tableSignature.SpacingBefore = 20f;
            tableSignature.SpacingAfter = 20f;
            tableSignature.HorizontalAlignment = Element.ALIGN_RIGHT;
            PdfPCell cellSignature = new PdfPCell() { MinimumHeight = 10, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell cellBottomSignature = new PdfPCell() { MinimumHeight = 40, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            cellSignature.Phrase = new Phrase("Paraf Penjual", normal_font);
            tableSignature.AddCell(cellSignature);
            cellSignature.Phrase = new Phrase("Paraf Pembeli", normal_font);
            tableSignature.AddCell(cellSignature);
            cellBottomSignature.Phrase = new Phrase(" ");
            tableSignature.AddCell(cellBottomSignature);
            cellBottomSignature.Phrase = new Phrase(" ");
            tableSignature.AddCell(cellBottomSignature);
            PdfPCell cellSignatures = new PdfPCell(tableSignature); // dont remove
            tableSignature.ExtendLastRow = false;
            tableSignature.SpacingAfter = 15f;
            document.Add(tableSignature);
            #endregion

            #region ConditionPage
            document.NewPage();

            string ConditionString = "D. Syarat Dan Ketentuan";
            Paragraph ConditionName = new Paragraph(ConditionString, bold_font) { Alignment = Element.ALIGN_LEFT };
            ConditionName.SpacingAfter = 1f;
            document.Add(ConditionName);

            List list = new List(List.ORDERED);
            list.IndentationLeft = 15f;
            list.Add(new ListItem("Kesepakatan",bold_font));

            List sublist1 = new List(List.ORDERED);
            sublist1.IndentationLeft = 10f;
            sublist1.PreSymbol = string.Format("{0}.", 1);
            ListItem one = new ListItem("Order yang telah diterima penjual tidak dapat dibatalkan secara sepihak oleh pembeli", normal_font);
            one.Alignment = Element.ALIGN_JUSTIFIED;
            one.Leading = 13;
            sublist1.Add(one);
            one = new ListItem("Setiap perubahan ketentuan dalam kontrak penjualan (apabila diperlukan) dapat dilakukan berdasarkan kesepakatan bersama", normal_font);
            one.Alignment = Element.ALIGN_JUSTIFIED;
            one.Leading = 13;
            sublist1.Add(one);
            list.Add(sublist1);

            list.Add(new ListItem("Keterlambatan Pembayaran Denda", bold_font));

            List sublist2 = new List(List.ORDERED);
            sublist2.IndentationLeft = 10f;
            sublist2.PreSymbol = string.Format("{0}.", 2);
            ListItem two = new ListItem("Bilamana terjadi keterlambatan pembayaran berdasarkan ketentuan pada huruf C angka 1, maka pembeli dikenakan denda sebesar " + viewModel.LatePayment + "% per bulan yang dihutang secara proporsi untuk keterlambatan per hari dari nominal yang belum dibayarkan, denda sekaligus pembayaran terutang tersebut harus dibayar secara tunai dan sekaligus lunas oleh pembeli", normal_font);
            two.Alignment= Element.ALIGN_JUSTIFIED;
            two.Leading = 13;
            sublist2.Add(two);
            two = new ListItem("Dalam hal Pembeli tidak dapat melakukan pembayaran beserta dendanya sampai dengan batas waktu yang ditentukan oleh penjual, maka tanpa mengesampingkan denda, Penjual dapat mengambil langkah sebagai berikut : ", normal_font);
            two.Alignment = Element.ALIGN_JUSTIFIED;
            two.Leading = 13;
            sublist2.Add(two);

            List bulletedlist = new List(List.UNORDERED, 10f);
            bulletedlist.IndentationLeft = 20f;
            two = new ListItem("Meminta Pembeli mengembalikan Produk yang belum dibayar dalam kondisi utuh dan lengkap, dalam hal ini Pembeli berkewajiban mengembalikan Produk sesuai permintaan Penjual dengan biaya ditanggung oleh Pembeli.", normal_font);
            two.Alignment = Element.ALIGN_JUSTIFIED;
            two.Leading = 13;
            bulletedlist.Add(two);
            two = new ListItem("Jika Pembeli tidak mengembalikan Produk dalam waktu " + viewModel.LateReturn + " hari setelah diminta oleh Penjual, maka Pembeli memberikan kuasa mutrlak dan tidak dapat dicabut kepada Penjual untuk mengambil kembali Produk yang belum dibayar oleh Pembeli dalam kondisi utuh dan lengkap seperti waktu pengiriman dari Penjual, segala biaya yang timbul dalam proses tersebut ditanggung Pembeli.", normal_font);
            two.Alignment = Element.ALIGN_JUSTIFIED;
            two.Leading = 13;
            bulletedlist.Add(two);
            two = new ListItem("Jika Produk sudah tidak ada karena sebab apapun maka Pembeli wajib mengganti dengan sejumlah uang senilai harga Produk.", normal_font);
            two.Alignment = Element.ALIGN_JUSTIFIED;
            two.Leading = 13;
            bulletedlist.Add(two);
            sublist2.Add(bulletedlist);
            list.Add(sublist2);

            list.Add(new ListItem("Klaim", bold_font));
            List sublist3 = new List(List.ORDERED);
            sublist3.IndentationLeft = 10f;
            sublist3.PreSymbol = string.Format("{0}.", 3);
            ListItem three = new ListItem("Jika Produk yang diterima Pembeli tidak seuai dengan kesepakatan, maka Pembeli wajib memberitahukan kepada Penjual, berikut dengan bukti yang cukup selambat-lambatnya " + viewModel.Claim + "(" + tanggalClaimTerbilang + ") hari setelah Produk diterima, selanjutnya klaim akan diselesaikan secara terpidah dan tidak dapat dihubungkan dan / atau diperhitungkan dengan pembayaran Produk dalam kontak Penjualan ini.", normal_font);
            three.Alignment = Element.ALIGN_JUSTIFIED;
            three.Leading = 13;
            sublist3.Add(three);
            three = new ListItem("Bilamana dalam jangka waktu tersebut diatas Pembeli tidak mengajukan klaim maka Produk dinyatakan sudah sesuai denan Kontrak Penjualan.", normal_font);
            three.Alignment = Element.ALIGN_JUSTIFIED;
            three.Leading = 13;
            sublist3.Add(three);
            list.Add(sublist3);

            list.Add(new ListItem("Force Majeure", bold_font));
            List sublist4 = new List(List.UNORDERED);
            sublist4.IndentationLeft = 10f;
            sublist4.ListSymbol = new Chunk("");
            ListItem four = new ListItem("Dalam hal terjadinya Force Majeure termasuk hal-hal berikut tetapi tidak terbatas pada bencana alam, kebakaran, pemogokan pekerjaan, hambatan lalu lintas, tindakan pemerintah dalam bidang ekonomi dan moneter yang ecara nyata berpengaruh terhadap pelaksanaan Kontrak Penjualan ini maupun hal-hal lain di luar kemampuan Penjual, maka Penjual tidak akan bertanggungjawab atas kegagalan penyerahan atau penyerahan yang tertunda, selanjutnya Penjual dan Pembeli sepakat untuk melakukan peninjauan kembali isi Kontrak Penjualan ini.", normal_font);
            four.Alignment = Element.ALIGN_JUSTIFIED;
            four.Leading = 13;
            sublist4.Add(four);
            list.Add(sublist4);

            list.Add(new ListItem("Perselisihan", bold_font));
            List sublist5 = new List(List.UNORDERED);
            sublist5.IndentationLeft = 10f;
            sublist5.ListSymbol=new Chunk("");
            ListItem five = new ListItem("Semua hal yang menyangkut adanya sengketa atau perselisihan semaksimal mungkin diselesaikan secara musyawarah. Jika tidak dapat tercapai mufakat maka Penjual dan Pembeli sepakat memilih domisili hukum yang umum dan tetap di Kantor Panitera Pengadilan Negeri Sukoharjo.", normal_font);
            five.Alignment = Element.ALIGN_JUSTIFIED;
            five.Leading = 13;
            sublist5.Add(five);
            list.Add(sublist5);

            document.Add(list);
            
            #endregion

            #region Signature
            PdfPTable signature = new PdfPTable(2);
            signature.SetWidths(new float[] { 1f, 1f });
            signature.SpacingBefore = 10f;
            PdfPCell cellIContentRights = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellIContentLefts = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cell_signature = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = /*Element.ALIGN_CENTER*/ Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            PdfPCell cell_signature_buyer = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER , VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            signature.SetWidths(new float[] { 1f, 1f });
            cell_signature.Phrase = new Phrase("Sukoharjo," + " " + viewModel.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy"/*, new CultureInfo("id - ID")*/) , normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Penjual,", normal_font);
            signature.AddCell(cell_signature);
            cell_signature_buyer.Phrase = new Phrase("Pembeli, ", normal_font);
            signature.AddCell(cell_signature_buyer);

            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);

            string signatureArea = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                signatureArea += Environment.NewLine;
            }

            cell_signature.Phrase = new Phrase(signatureArea, normal_font);
            signature.AddCell(cell_signature);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase(" ", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase(" ", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase(" ", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase(" ", normal_font);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("Robby O S", normal_font);
            signature.AddCell(cell_signature);
            cell_signature_buyer.Phrase = new Phrase(UppercaseWords(viewModel.Buyer.BuyerOwner) , normal_font);
            signature.AddCell(cell_signature_buyer);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cellIContentRights.Phrase = new Phrase("");
            signature.AddCell(cellIContentRights);

            PdfPCell signatureCell = new PdfPCell(signature); // dont remove
            signature.ExtendLastRow = false;
            signature.SpacingAfter = 10f;
            document.Add(signature);
            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        //static string UppercaseWords(string value1)
        //{
        //    if (value1 != null)
        //    {
        //        string value = value1.ToLower();
        //        //string value = ("PT. DAN LIRIS").ToLower();
        //        char[] array = value.ToCharArray();
        //        // Handle the first letter in the string.
        //        if (array.Length >= 1)
        //        {
        //            if (char.IsLower(array[0]))
        //            {
        //                array[0] = char.ToUpper(array[0]);
        //            }
        //        }
        //        // Scan through the letters, checking for spaces.
        //        // ... Uppercase the lowercase letters following spaces.
        //        for (int i = 1; i < array.Length; i++)
        //        {
        //            if (array[i - 1] == ' ' || array[i - 1] == '\n')
        //            {
        //                if (char.IsLower(array[i]))
        //                {
        //                    array[i] = char.ToUpper(array[i]);
        //                }
        //            }
        //        }
        //        return new string(array);
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //    //return new string(array);
        //}

        static string UppercaseWords(string value1)
        {
            if (value1 != null)
            {
                string value = value1.ToLower();
                //string value = ("PT. DAN LIRIS").ToLower();
                char[] array = value.ToCharArray();
                // Handle the first letter in the string.
                if (array.Length >= 1)
                {
                    if (char.IsLower(array[0]))
                    {
                        array[0] = char.ToUpper(array[0]);
                    }
                }
                // Scan through the letters, checking for spaces.
                // ... Uppercase the lowercase letters following spaces.
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i - 1] == ' ' || array[i - 1] == '\n')
                    {
                        if (char.IsLower(array[i]))
                        {
                            array[i] = char.ToUpper(array[i]);
                        }
                    } else if (array[i] == 't' && array[i - 1] == 'P')
                    {
                        if (char.IsLower(array[i]))
                        {
                            array[i] = char.ToUpper(array[i]);
                        }
                    }
                }
                return new string(array);
            }
            else
            {
                return "";
            }
            //return new string(array);
        }

        class FPSalesContractWithHeaderPDFTemplatePageEvent : iTextSharp.text.pdf.PdfPageEventHelper
        {
            private FinishingPrintingSalesContractViewModel viewModel;
            private int timeoffset;
            private Image borderImage;

            public FPSalesContractWithHeaderPDFTemplatePageEvent(FinishingPrintingSalesContractViewModel viewModel, int timeoffset)
            {
                this.viewModel = viewModel;
                this.timeoffset = timeoffset;
                string borderBase64 = "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";
                byte[] imageBytes = Convert.FromBase64String(borderBase64);
                borderImage = Image.GetInstance(imageBytes);
            }

            public override void OnStartPage(PdfWriter writer, Document document)
            {
                PdfContentByte cb = writer.DirectContent;
                cb.BeginText();
                Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);
                Font big_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14, Font.BOLD);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7, Font.UNDERLINE);

                float height = writer.PageSize.Height, width = writer.PageSize.Width;
                float marginLeft = document.LeftMargin - 10, marginTop = document.TopMargin, marginRight = document.RightMargin - 10;

                cb.SetFontAndSize(bf, 7);


                #region LOGODL

                byte[] imageByteDL = Convert.FromBase64String(Base64ImageStrings.LOGO_DANLIRIS_58_58);
                Image imageDL = Image.GetInstance(imageByteDL);
                if (imageDL.Width > 60)
                {
                    float percentage = 0.0f;
                    percentage = 60 / imageDL.Width;
                    imageDL.ScalePercent(percentage * 100);
                }
                imageDL.SetAbsolutePosition(marginLeft, height - imageDL.ScaledHeight - marginTop + 90);
                cb.AddImage(imageDL, inlineImage: true);

                #endregion

                #region ADDRESS

                var headOfficeX = width / 2 + 30;
                var headOfficeY = height - marginTop + 45;

                var branchOfficeY = height - marginTop + 85;

                byte[] imageByte = Convert.FromBase64String(Base64ImageStrings.LOGO_NAME);
                Image image1 = Image.GetInstance(imageByte);
                if (image1.Width > 100)
                {
                    float percentage = 0.0f;
                    percentage = 100 / image1.Width;
                    image1.ScalePercent(percentage * 100);
                }
                image1.SetAbsolutePosition(marginLeft + 80, height - image1.ScaledHeight - marginTop + 105);
                cb.AddImage(image1, inlineImage: true);

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Head Office : Jl. Merapi No. 23", marginLeft + 80, branchOfficeY, 0);
                string[] branchOffices = {
                "Banaran, Grogol, Sukoharjo 57552",
                "Central Java, Indonesia",
                "Tel. : (+62-271) 740888, 714400",
                "Fax. : (+62-271) 740777, 735222",
                "PO BOX 166 Solo, 57100",
                "Website : www.danliris.com",
            };
                for (int i = 0; i < branchOffices.Length; i++)
                {
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, branchOffices[i], marginLeft + 80, branchOfficeY - 10 - (i * 10), 0);
                }

                #endregion

                #region LOGOISO

                byte[] imageByteIso = Convert.FromBase64String(Base64ImageStrings.ISO);
                Image imageIso = Image.GetInstance(imageByteIso);
                if (imageIso.Width > 100)
                {
                    float percentage = 0.0f;
                    percentage = 100 / imageIso.Width;
                    imageIso.ScalePercent(percentage * 100);
                }
                imageIso.SetAbsolutePosition(width - imageIso.ScaledWidth - marginRight, height - imageIso.ScaledHeight - marginTop + 90);
                cb.AddImage(imageIso, inlineImage: true);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CERTIFICATE ID09 / 01238", width - (imageIso.ScaledWidth / 2) - marginRight, height - imageIso.ScaledHeight - marginTop + 60 - 5, 0);

                #endregion

                cb.EndText();
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfContentByte cb = writer.DirectContent;

                //// Set border image to fit the page
                //borderImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                //borderImage.SetAbsolutePosition(0, 0); // Set posisi di sudut kiri bawah

                // Dapatkan ukuran halaman dari dokumen
                float pageWidth = document.PageSize.Width;
                float pageHeight = document.PageSize.Height;

                // Set tinggi border agar sesuai dengan ukuran halaman
                // Tambahkan sedikit ekstra tinggi jika diperlukan
                float borderHeight = pageHeight;

                // Atur scaling dan posisi border
                borderImage.ScaleAbsolute(pageWidth, borderHeight-95);
                borderImage.SetAbsolutePosition(0, 0); // Geser ke bawah untuk menutupi margin


                // Tambahkan border image sebagai latar belakang halaman
                cb.AddImage(borderImage);

                // Add additional header content
                cb.BeginText();
                Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);
                Font big_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 14, Font.BOLD);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                Font normal_font_underlined = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7, Font.UNDERLINE);

                float height = writer.PageSize.Height, width = writer.PageSize.Width;
                float marginLeft = document.LeftMargin - 10, marginTop = document.TopMargin, marginRight = document.RightMargin - 10;

                cb.SetFontAndSize(bf, 7);

                // Teks tambahan di header atau footer jika diperlukan
                // Contoh teks logo, alamat, atau informasi lainnya di atas border

                cb.EndText();
            }

        }
    }
}
