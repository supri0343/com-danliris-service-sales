using Com.Danliris.Service.Sales.Lib.ViewModels.Weaving;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Com.Danliris.Service.Sales.Lib.Utilities;

namespace Com.Danliris.Service.Sales.Lib.PDFTemplates
{
    public class NewWeavingSalesContractPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(WeavingSalesContractViewModel viewModel, int timeoffset)
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
            Document document = new Document(PageSize.A4, 50, 50, 80, 40);
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region CustomViewModel
            string QuantityToText = NumberToTextIDN.terbilang(viewModel.OrderQuantity);
            var amount = viewModel.Price * viewModel.OrderQuantity;
            string AmountToText = NumberToTextEN.toWords(amount);

            var uom = "";
            var uom1 = "";
            //if (viewModel.Uom.Unit.ToLower() == "yds")
            if (viewModel.Uom.Unit.ToLower() == "yds")
            {
                uom = "YARD";
                uom1 = "YARDS";
            }
            else if (viewModel.Uom.Unit.ToLower() == "mtr")
            {
                uom = "METER";
                uom1 = "METERS";
            }
            else
            {
                uom = viewModel.Uom.Unit;
                uom = viewModel.Uom.Unit;
            }

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
            bodyContentLeft.Phrase = new Phrase(": HENDRO SUSENO ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Jabatan", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": Penjualan Textile", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Alamat", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": Jl.Merapi No.23 Banaran, Grogol, Sukoharjo, 57552 ", normal_font);
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
            bodyContentLefts.Phrase = new Phrase("Nama", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase(": " + "" + viewModel.Buyer.Name, normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("NIK ", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase(": " + " " + viewModel.Buyer.NIK, normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("Pekerjaan ", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase(":" + " " + viewModel.Buyer.Job, normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase("Alamat", normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            bodyContentLefts.Phrase = new Phrase(":" + " " + viewModel.Buyer.Address, normal_font);
            tableBodyBuyer.AddCell(bodyContentLefts);
            PdfPCell cellBodys = new PdfPCell(tableBodyBuyer); // dont remove
            tableBodyBuyer.ExtendLastRow = false;
            tableBodyBuyer.SpacingAfter = 0.5f;
            document.Add(tableBodyBuyer);

            string ParagraphStringbuyer = "          Bertindak untuk dan atas nama " + "" + viewModel.Buyer.Name + "" + ", selanjutnya disebut pembeli";
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

            string ParagraphString3 = "A. PRODUK YANG DIORDER";
            Paragraph Paragraph3 = new Paragraph(ParagraphString3, bold_font) { Alignment = Element.ALIGN_LEFT };
            Paragraph3.SpacingAfter = 4f;
            document.Add(Paragraph3);

            //#region Produk diorder
            PdfPTable tableOrder = new PdfPTable(2);
            tableOrder.TotalWidth = 300f;
            tableOrder.LockedWidth = true;
            float[] widths = new float[] { 5f, 5f };
            tableOrder.SetWidths(widths);
            tableOrder.HorizontalAlignment = 0;
            tableOrder.SpacingBefore = 10f;
            tableOrder.SpacingAfter = 30f;
            PdfPCell cellOrder = new PdfPCell() { MinimumHeight = 10, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };

            cellOrder.Phrase = new Phrase("Jenis Produk", bold_font);
            tableOrder.AddCell(cellOrder);
            cellOrder.Phrase = new Phrase("Material/Konstruksi", bold_font);
            tableOrder.AddCell(cellOrder);
            cellOrder.Phrase = new Phrase(viewModel.ProductType.Name, normal_font);
            tableOrder.AddCell(cellOrder);
            cellOrder.Phrase = new Phrase(viewModel.Material.Name + "" + "-" + " " + viewModel.MaterialConstruction.Name, normal_font);
            tableOrder.AddCell(cellOrder);
            tableOrder.AddCell(cellOrder);



            PdfPCell cellProduct = new PdfPCell(tableOrder); // dont remove
            tableOrder.ExtendLastRow = false;
            tableOrder.SpacingAfter = 10f;
            document.Add(tableOrder);

            //cellOrder.VerticalAlignment = Element.ALIGN_TOP;
            //tableOrder.AddCell(cellOrder);

            //tableOrder.SpacingAfter = 10;
            //document.Add(tableOrder);
            //#endregion

            string ParagraphString4 = "B. KESEPAKATAN ORDER";
            Paragraph Paragraph4 = new Paragraph(ParagraphString4, bold_font) { Alignment = Element.ALIGN_LEFT };
            Paragraph4.SpacingAfter = 4f;
            document.Add(Paragraph4);

            //#region Pemenuhan Order
            PdfPTable tableDetailOrder = new PdfPTable(2);
            //tableDetailOrder.WidthPercentage = 20;
            //tableDetailOrder.SetWidths(new float[] { 20f, 20f });
            tableDetailOrder.TotalWidth = 216f;
            tableDetailOrder.LockedWidth = true;
            float[] widthsDetail = new float[] { 1f, 1f };
            tableDetailOrder.SetWidths(widthsDetail);
            tableDetailOrder.HorizontalAlignment = 0;
            tableDetailOrder.SpacingBefore = 10f;
            tableDetailOrder.SpacingAfter = 20f;
            PdfPCell cellDetailOrder = new PdfPCell() { MinimumHeight = 10, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_MIDDLE };
            PdfPCell CellDetailCenter = new PdfPCell() { MinimumHeight = 10, Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            cellDetailOrder.Phrase = new Phrase("Jumlah", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(viewModel.OrderQuantity.ToString("N2") + " (" + QuantityToText + ") " + uom, normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Harga", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            cellDetailOrder.Phrase = new Phrase("Total Harga", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            //cellDetailOrder.Phrase = new Phrase(Convert.ToString(viewModel.Amount), normal_font);
            CellDetailCenter.Phrase = new Phrase(string.Format("{0:n2}", amount)  + AmountToText , normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Jenis Packing", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(viewModel.Packing, normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Jadwal Pengiriman", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(viewModel.TermOfShipment, normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Ongkos Angkut", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(viewModel.TransportFee, normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            cellDetailOrder.Phrase = new Phrase("Alamat Pengiriman", bold_font);
            tableDetailOrder.AddCell(cellDetailOrder);
            CellDetailCenter.Phrase = new Phrase(viewModel.DeliveredTo, normal_font);
            tableDetailOrder.AddCell(CellDetailCenter);
            //CheckBox checkBox1 = new CheckBox(20, 20, 15, 15, "checkBox1");
            //page.Annotations.Add(checkBox1);


            PdfPCell cellDetail = new PdfPCell(tableDetailOrder); // dont remove
            tableDetailOrder.ExtendLastRow = false;
            tableDetailOrder.SpacingAfter = 10f;
            document.Add(tableDetailOrder);

            cellDetailOrder.VerticalAlignment = Element.ALIGN_TOP;
            tableDetailOrder.AddCell(cellDetailOrder);

            //cellDetailOrder.VerticalAlignment = Element.ALIGN_TOP;
            //tableDetailOrder.AddCell(cellDetailOrder);

            //tableDetailOrder.SpacingAfter = 10;
            //document.Add(tableDetailOrder);

            string ParagraphString5 = "C. Metode Pembayaran";
            Paragraph Paragraph5 = new Paragraph(ParagraphString5, bold_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(Paragraph5);


            //PdfPTable tablePembayaran = new PdfPTable(4);
            PdfPTable tablePembayaran = new PdfPTable(3);
            tablePembayaran.SetWidths(new float[] { 0.01f, 0.01f, 0.03f });
            PdfPCell bodyContentPembayaran = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            bodyContentPembayaran.Phrase = new Phrase("1. Cara Pembayaran", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(":", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(viewModel.PaymentMethods, normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);

            bodyContentPembayaran.Phrase = new Phrase("2. Down Payment (DP)", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(":", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(viewModel.DownPayments, normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);

            bodyContentPembayaran.Phrase = new Phrase("3. Rekening Tujuan Pembayaran", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(":", normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            bodyContentPembayaran.Phrase = new Phrase(viewModel.AccountBank.AccountNumber, normal_font);
            tablePembayaran.AddCell(bodyContentPembayaran);
            //bodyContentPembayaran.Phrase = new Phrase("4. Pembayaran dianggap sah / lunas jika diterima penjual sesusai dengan nilai tagihan.", normal_font);
            //tablePembayaran.AddCell(bodyContentPembayaran);
            //bodyContentPembayaran.Phrase = new Phrase("", normal_font);
            //tablePembayaran.AddCell(bodyContentPembayaran);
            //bodyContentPembayaran.Phrase = new Phrase("", normal_font);
            //tablePembayaran.AddCell(bodyContentPembayaran);
            PdfPCell cellPembayaran = new PdfPCell(tablePembayaran); // dont remove
            tablePembayaran.ExtendLastRow = false;
            tablePembayaran.SpacingAfter = 0.5f;
            document.Add(tablePembayaran);
            string ParagraphStringPembayaran = "4. Pembayaran dianggap sah / lunas jika diterima penjual sesusai dengan nilai tagihan.";
            Paragraph ParagraphPembayaran = new Paragraph(ParagraphStringPembayaran, normal_font) { Alignment = Element.ALIGN_LEFT };
            tablePembayaran.SpacingAfter = 30f;
            document.Add(ParagraphPembayaran);


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

            string ConditionString = "D. SYARAT DAN KETENTUAN";
            Paragraph ConditionName = new Paragraph(ConditionString, bold_font) { Alignment = Element.ALIGN_LEFT };
            ConditionName.SpacingAfter = 1f;
            document.Add(ConditionName);

            //PdfPTable conditionList = new PdfPTable(2);
            //conditionList.SetWidths(new float[] { 0.01f, 1f });
            string DashListSymbol = "\u00AF";
            PdfPCell cellIContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell bodyContentJustify = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_JUSTIFIED };
            PdfPCell cellIContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };

            PdfPTable conditionKesepakatan = new PdfPTable(2);
            conditionKesepakatan.SetWidths(new float[] { 0.04f, 1f });

            bodyContentJustify.SetLeading(1.5f, 1);
            cellIContentLeft.Phrase = new Phrase("1. ", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("Kesepakatan ", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentRight.Phrase = new Phrase("1.1 ", normal_font);
            conditionKesepakatan.AddCell(cellIContentRight);
            bodyContentJustify.Phrase = new Phrase("Order yang telah diterima penjual tidak dapat dibatalkan secara sepohak oleh pembeli ", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentLeft.Phrase = new Phrase("1.2 ", normal_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            bodyContentJustify.Phrase = new Phrase("Setiap perubahan ketentuan dalam kontrak penjualan (apabila diperlukan) dapat dilakukan berdasarkan kesepakatan bersama ", normal_font);


            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentLeft.Phrase = new Phrase("2. ", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("Keterlambatan Pembayaran dan Denda ", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("2.1 ", normal_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            bodyContentJustify.Phrase = new Phrase("Bilamana terjadi keterlambatan pembayaran berdasarkan ketentuan pada huruf C angka 1, maka pembeli dikenakan denda sebesar .... % per bulan yang dihutang secara proporsi untuk keterlambatan per hari dari nominal yang belum dibayarkan, denda sekaligus pembayaran terutang tersebut harus dibayar secara tunai dan sekaligus lunas oleh pembeli", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentLeft.Phrase = new Phrase("2.2 ", normal_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            bodyContentJustify.Phrase = new Phrase("Dalam hal Pembeli tidak dapat melakukan pembayaran beserta dendanya sampai dengan batas waktu yang ditentukan oleh penjual, maka tanpa mengesampingkan denda, Penjual dapat mengambil langkah sebagai berikut : ", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentRight.Phrase = new Phrase(DashListSymbol, normal_font1);
            conditionKesepakatan.AddCell(cellIContentRight);
            bodyContentJustify.Phrase = new Phrase("Meminta Pembeli mengembalikan Produk yang belum dibayar dalam kondisi utuh dan lengkap, dalam hal ini Pembeli berkewajiban mengembalikan Produk sesuai permintaan Penjual dengan biaya ditanggung oleh Pembeli.", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentRight.Phrase = new Phrase(DashListSymbol, normal_font1);
            conditionKesepakatan.AddCell(cellIContentRight);
            bodyContentJustify.Phrase = new Phrase("Jika Pembeli tidak mengembalikan Produk dalam waktu ..... hari setelah diminta oleh Penjual, maka Pembeli memberikan kuasa mutrlak dan tidak dapat dicabut kepada Penjual untuk mengambil kembali Produk yang belum dibayar oleh Pembeli dalam kondisi utuh dan lengkap seperti waktu pengiriman dari Penjual, segala biaya yang timbul dalam proses tersebut ditanggung Pembeli.", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentRight.Phrase = new Phrase(DashListSymbol, normal_font1);
            conditionKesepakatan.AddCell(cellIContentRight);
            bodyContentJustify.Phrase = new Phrase("Jika Produk sudah tidak ada karena sebab apapun maka Pembeli wajib mengganti dengan sejumlah uang senilai harga Produk.", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentLeft.Phrase = new Phrase("3. ", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("Klaim ", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("3.1 ", normal_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            bodyContentJustify.Phrase = new Phrase("Jika Produk yang diterima Pembeli tidak seuai dengan kesepakatan, maka Pembeli wajib memberitahukan kepada Penjual, berikut dengan bukti yang cukup selambat-lambatnya ......(......) hari setelah Produk diterima, selanjutnya klaim akan diselesaikan secara terpidah dan tidak dapat dihubungkan dan / atau diperhitungkan dengan pembayaran Produk dalam kontak Penjualan ini.", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentLeft.Phrase = new Phrase("3.2 ", normal_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            bodyContentJustify.Phrase = new Phrase("Bilamana dalam jangka waktu tersebut diatas Pembeli tidak mengajukan klaim maka Produk dinyatakan sudah sesuai denan Kontrak Penjualan.", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentLeft.Phrase = new Phrase("4. ", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("Force Majeure", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            bodyContentJustify.Phrase = new Phrase("Dalam hal terjadinya Force Majeure termasuk hal-hal berikut tetapi tidak terbatas pada bencana alam, kebakaran, pemogokan pekerjaan, hambatan lalu lintas, tindakan pemerintah dalam bidang ekonomi dan moneter yang ecara nyata berpengaruh terhadap pelaksanaan Kontrak Penjualan ini maupun hal-hal lain di luar kemampuan Penjual, maka Penjual tidak akan bertanggungjawab atas kegagalan penyerahan atau penyerahan yang tertunda, selanjutnya Penjual dan Pembeli sepakat untuk melakukan peninjauan kembali isi Kontrak Penjualan ini.", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            cellIContentLeft.Phrase = new Phrase("5. ", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("Perselisihan", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            cellIContentLeft.Phrase = new Phrase("", bold_font);
            conditionKesepakatan.AddCell(cellIContentLeft);
            bodyContentJustify.Phrase = new Phrase("Semua hal yang menyangkut adanya sengketa atau perselisihan semaksimal mungkin diselesaikan secara musyawarah. Jika tidak dapat tercapai mufakat maka Penjual dan Pembeli sepakat memilih domisili hukum yang umum dan tetap di Kantor Panitera Pengadilan Negeri Sukoharjo.", normal_font);
            conditionKesepakatan.AddCell(bodyContentJustify);
            PdfPCell conditionListData = new PdfPCell(conditionKesepakatan); // dont remove
            conditionKesepakatan.ExtendLastRow = false;
            conditionKesepakatan.SpacingAfter = 20f;
            document.Add(conditionKesepakatan);


            //PdfPTable conditionListChild = new PdfPTable(2);
            //conditionListChild.SetWidths(new float[] { 0.04f, 1f });
            //cellIContentLeft.Phrase = new Phrase("1.1 ", normal_font);
            //conditionKesepakatan.AddCell(cellIContentLeft);
            //cellIContentLeft.Phrase = new Phrase("Order yang telah diterima penjual tidak dapat dibatalkan secara sepohak oleh pembeli ", normal_font);
            //conditionKesepakatan.AddCell(cellIContentLeft);
            //cellIContentLeft.Phrase = new Phrase("1.2 ", normal_font);
            //conditionKesepakatan.AddCell(cellIContentLeft);
            //cellIContentLeft.Phrase = new Phrase("Setiap perubahan ketentuan dalam kontrak penjualan (apabila diperlukan) dapat dilakukan berdasarkan kesepakatan bersama ", normal_font);
            //conditionKesepakatan.AddCell(cellIContentLeft);
            //PdfPCell conditionListChildData = new PdfPCell(conditionListChild); // dont remove
            //conditionListChild.ExtendLastRow = false;
            //conditionListChild.SpacingAfter = 1f;
            //document.Add(conditionListChild);
            #endregion

            #region Signature
            PdfPTable signature = new PdfPTable(2);
            signature.SetWidths(new float[] { 1f, 1f });
            PdfPCell cellIContentRights = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            PdfPCell cellIContentLefts = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cell_signature = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = /*Element.ALIGN_CENTER*/ Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            signature.SetWidths(new float[] { 1f, 1f });
            cell_signature.Phrase = new Phrase("Sukoharjo," + " " + viewModel.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy"/*, new CultureInfo("id - ID")*/), normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Penjual,", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Pembeli, ", normal_font);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);

            string signatureArea = string.Empty;
            for (int i = 0; i < 5; i++)
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

            cell_signature.Phrase = new Phrase("( HENDRO SUSENO )", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("(" + viewModel.Buyer.Name + ")", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Penjualan Tekstil", normal_font);
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
    }
}
