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
    public class FinishingPrintingSalesContractExportPDFTemplateWcop
    {
        public MemoryStream GeneratePdfTemplate(FinishingPrintingSalesContractViewModel viewModel, int timeoffset)
        {
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, 40, 40, 135, 10);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new FPSalesContractExportWithHeaderPDFTemplatePageEvent(viewModel, timeoffset);
            document.Open();


            #region customViewModel

            var uom = "";
            var uom1 = "";
            List<string> details = new List<string>();
            //double convertion = 0;

            if (viewModel.UOM.Unit.ToLower() == "yds")
            {
                uom = "YARDS";
                uom1 = "YARD";
            }
            else if (viewModel.UOM.Unit.ToLower() == "mtr")
            {
                uom = "METRES";
                uom1 = "METRE";
            }
            else
            {
                uom = viewModel.UOM.Unit;
                uom1 = viewModel.UOM.Unit;
            }


            string QuantityToText = NumberToTextEN.toWords((double)viewModel.OrderQuantity);
            double amount = ((double)viewModel.Amount);
            string AmountToText = NumberToTextEN.toWords(amount);

            //var detailprice = viewModel.AccountBank.Currency.Symbol + " " + string.Format("{0:n2}", viewModel.Price) + " / KG";

            var appx = "";
            var date = viewModel.DeliverySchedule.Value.Day;
            if (date >= 1 && date <= 10)
            {
                appx = "EARLY";
            }
            else if (date >= 11 && date <= 20)
            {
                appx = "MIDDLE";
            }
            else if (date >= 21 && date <= 31)
            {
                appx = "END";
            }

            List<string> newDetail = new List<string>();

            foreach (var i in viewModel.Details)
            {
                //var ppn = "";
                //if ((bool)viewModel.UseIncomeTax)
                //{
                //    if (i.UseIncomeTax)
                //    {
                //        ppn = "INCLUDING PPN 10%";
                //    }
                //    else
                //    {
                //        ppn = "EXCLUDING PPN";
                //    }
                //}
                //else
                //{
                //    ppn = "TANPA PPN";
                //}
                var nominal = string.Format("{0:n2}", i.Price);

                if (i.Currency.Code.ToLower() == "usd")
                {
                    nominal = string.Format("{0:n2}", i.Price);
                }

                details.Add(i.Color + " " + i.Currency.Symbol + " " + nominal + " / " + uom1 + " ");
            }

            #endregion

            #region Header

            string codeNoString = "FM-PJ-00-03-004/R2";
            Paragraph codeNo = new Paragraph(codeNoString, bold_font) { Alignment = Element.ALIGN_RIGHT };
            document.Add(codeNo);
            Paragraph dateString = new Paragraph($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("MMMM dd, yyyy", new CultureInfo("en-US"))}", normal_font) { Alignment = Element.ALIGN_RIGHT };
            dateString.SpacingAfter = 5f;
            document.Add(dateString);

            #region Identity

            //PdfPTable tableIdentity = new PdfPTable(3);
            //tableIdentity.SetWidths(new float[] { 0.5f, 4.5f, 2.5f });

            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy", new CultureInfo("en-US"))}", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentRight.Phrase = new Phrase("");
            //tableIdentity.AddCell(cellIdentityContentRight);
            //PdfPCell cellIdentity = new PdfPCell(tableIdentity); // dont remove
            //tableIdentity.ExtendLastRow = false;
            //tableIdentity.SpacingAfter = 10f;
            //document.Add(tableIdentity);

            PdfPTable tableIdentityOpeningLetter = new PdfPTable(3);
            tableIdentityOpeningLetter.SetWidths(new float[] { 3f, 1f, 1f });
            PdfPCell cellIdentityContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            cellIdentityContentLeft.Phrase = new Phrase("MESSRS,", normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("", normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Name, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Address, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.City, normal_font);
            //tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase("");
            //tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase("");
            //tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Country?.ToUpper(), normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Contact, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            PdfPCell cellIdentityOpeningLetter = new PdfPCell(tableIdentityOpeningLetter); // dont remove
            tableIdentityOpeningLetter.ExtendLastRow = false;
            tableIdentityOpeningLetter.SpacingAfter = 10f;
            document.Add(tableIdentityOpeningLetter);

            #endregion

            string titleString = "SALES CONTRACT";
            Paragraph title = new Paragraph(titleString, bold_font) { Alignment = Element.ALIGN_CENTER };
            title.SpacingAfter = 10f;
            document.Add(title);
            bold_font.SetStyle(Font.NORMAL);

            #endregion



            #region HeaderParagraphString
            string HeaderParagraphString = "On behalf of :";
            Paragraph HeaderParagraph = new Paragraph(HeaderParagraphString, normal_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(HeaderParagraph);

            string firstParagraphString = "PT. DAN LIRIS JL. MERAPI NO. 23 BANARAN, GROGOL, SUKOHARJO, 57552, CENTRAL JAVA – INDONESIA, we confrm the order under the following terms and conditions as mentioned below: ";
            Paragraph firstParagraph = new Paragraph(firstParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
            firstParagraph.SpacingAfter = 10f;
            document.Add(firstParagraph);
            #endregion

            #region body
            PdfPTable tableBody = new PdfPTable(2);
            tableBody.SetWidths(new float[] { 0.75f, 2f });
            PdfPCell bodyContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell bodyContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell bodyContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            bodyContentLeft.Phrase = new Phrase("Contract Number", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.SalesContractNo, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Comodity", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Material.Name + " " + viewModel.MaterialConstruction.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);

            if (!string.IsNullOrEmpty(viewModel.YarnMaterial.Name) && !string.IsNullOrEmpty(viewModel.MaterialWidth) && !string.IsNullOrWhiteSpace(viewModel.YarnMaterial.Name) && !string.IsNullOrWhiteSpace(viewModel.MaterialWidth))
            {
                bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase("  " + viewModel.YarnMaterial.Name + " WIDTH: " + viewModel.MaterialWidth, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }

            if (!string.IsNullOrEmpty(viewModel.CommodityDescription) && !string.IsNullOrWhiteSpace(viewModel.CommodityDescription))
            {
                bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase("  " + viewModel.CommodityDescription, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }

            bodyContentLeft.Phrase = new Phrase("Quality", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Quality.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Quantity", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.OrderQuantity.GetValueOrDefault().ToString("N2") + " ( " + QuantityToText + " ) " + uom, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Piece Length", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.PieceLength, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Price & Payment", normal_font);
            tableBody.AddCell(bodyContentLeft);

            int index = 0;

            foreach (var detail in details)
            {
                index++;
                if (index == 1)
                {
                    bodyContentLeft.Phrase = new Phrase(": " + detail, normal_font);
                    tableBody.AddCell(bodyContentLeft);
                }
                else
                {
                    bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                    tableBody.AddCell(bodyContentLeft);
                    bodyContentLeft.Phrase = new Phrase("  " + detail, normal_font);
                    tableBody.AddCell(bodyContentLeft);
                }
            }
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  " + viewModel.TermOfShipment, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  " + viewModel.TermOfPayment.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Amount", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.AccountBank.Currency.Symbol + " " + string.Format("{0:n2}", amount) + " ( " + AmountToText + " " + viewModel.AccountBank.Currency.Description?.ToUpper() + " ) (APPROXIMATELLY)", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Shipment", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + appx + " " + (viewModel.DeliverySchedule.Value.AddHours(timeoffset).ToString("MMMM yyyy", new CultureInfo("en-US")))?.ToUpper(), normal_font);
            tableBody.AddCell(bodyContentLeft);

            if (!string.IsNullOrEmpty(viewModel.ShipmentDescription) && !string.IsNullOrWhiteSpace(viewModel.ShipmentDescription))
            {
                bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase("  " + viewModel.ShipmentDescription, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }

            bodyContentLeft.Phrase = new Phrase("Destination", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.DeliveredTo, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Packing", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Packing, normal_font);
            tableBody.AddCell(bodyContentLeft);
            PdfPCell cellBody = new PdfPCell(tableBody); // dont remove
            tableBody.ExtendLastRow = false;
            document.Add(tableBody);

            PdfPTable conditionListBody = new PdfPTable(3);
            conditionListBody.SetWidths(new float[] { 0.4f, 0.025f, 1f });

            bodyContentLeft.Phrase = new Phrase("Condition", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("THIS CONTRACT IS IRREVOCABLE UNLESS AGREED UPON BY THE TWO PARTIES, THE BUYER AND SELLER.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("+/- " + viewModel.ShippingQuantityTolerance + " % FROM QUANTITY ORDER SHOULD BE ACCEPTABLE.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("CONTAINER DELIVERY CHARGES AT DESTINATION FOR BUYER'S ACCOUNT.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("- ", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase(viewModel.Condition, normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentRight.Phrase = new Phrase("");
            conditionListBody.AddCell(bodyContentRight);
            PdfPCell cellConditionList = new PdfPCell(conditionListBody); // dont remove
            conditionListBody.ExtendLastRow = false;
            conditionListBody.SpacingAfter = 10f;
            document.Add(conditionListBody);

            #endregion

            #region signature
            PdfPTable signature = new PdfPTable(2);
            signature.SetWidths(new float[] { 1f, 1f });
            PdfPCell cell_signature = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            signature.SetWidths(new float[] { 1f, 1f });
            cell_signature.Phrase = new Phrase("Accepted and confrmed :", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("PT DANLIRIS", normal_font);
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

            cell_signature.Phrase = new Phrase("(...........................)", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("ROBBY O S", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Authorized signature", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Marketing Textile", normal_font);
            signature.AddCell(cell_signature);
            cellIdentityContentRight.Phrase = new Phrase("");
            signature.AddCell(cellIdentityContentRight);

            PdfPCell signatureCell = new PdfPCell(signature); // dont remove
            signature.ExtendLastRow = false;
            signature.SpacingAfter = 1f;
            document.Add(signature);
            #endregion

            #region ConditionPage
            document.NewPage();

            string ConditionString = "Remark";
            Paragraph ConditionName = new Paragraph(ConditionString, header_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(ConditionName);

            string bulletListSymbol = "\u2022";
            PdfPCell bodyContentJustify = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_JUSTIFIED };

            PdfPTable conditionList = new PdfPTable(2);
            conditionList.SetWidths(new float[] { 0.01f, 1f });

            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("All instructions regarding sticker, shipping marks etc. to be received 1 (one) month prior to shipment.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //conditionList.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Benefciary :  PT. DAN LIRIS JL. MERAPI NO. 23 BANARAN, GROGOL, SUKOHARJO, 57552, CENTRAL JAVA – INDONESIA  (Phone No. 0271 - 740888 / 714400). ", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("Payment Transferred to: ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("PAYMENT TO BE TRANSFERRED TO BANK " + viewModel.AccountBank.BankName, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);

            if (!string.IsNullOrEmpty(viewModel.AccountBank.BankAddress))
            {
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                conditionList.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.AccountBank.BankAddress, normal_font);
                conditionList.AddCell(cellIdentityContentLeft);
            }

            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("ACCOUNT NAME : " + viewModel.AccountBank.AccountName, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("ACCOUNT NO : " + viewModel.AccountBank.AccountNumber + " SWIFT CODE : " + viewModel.AccountBank.SwiftCode, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase(viewModel.TermOfPayment.Name + " to be negotiable with BANK " + viewModel.AccountBank.BankName, normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Please find enclosed some Indonesia Banking Regulations.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("If you find anything not order, please let us know immediately.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            PdfPCell conditionListData = new PdfPCell(conditionList); // dont remove
            conditionList.ExtendLastRow = false;
            document.Add(conditionList);
            #endregion

            #region agentTemplate
            if (viewModel.Agent.Id != 0)
            {
                document.NewPage();

                #region Identity
                PdfPTable agentIdentity = new PdfPTable(3);
                agentIdentity.SetWidths(new float[] { 0.5f, 4.5f, 2.5f });
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("MMMM dd, yyyy", new CultureInfo("en-US"))}", normal_font);
                agentIdentity.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentity.AddCell(cellIdentityContentRight);
                PdfPCell agentCellIdentity = new PdfPCell(agentIdentity); // dont remove
                agentIdentity.ExtendLastRow = false;
                agentIdentity.SpacingAfter = 10f;
                document.Add(agentIdentity);

                PdfPTable agentIdentityOpeningLetter = new PdfPTable(3);
                agentIdentityOpeningLetter.SetWidths(new float[] { 3f, 1f, 1f });
                cellIdentityContentLeft.Phrase = new Phrase("MESSRS,", normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Name, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Address, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                //if (!string.IsNullOrEmpty(viewModel.Agent.City))
                //{
                //    cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.City, normal_font);
                //    agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                //    cellIdentityContentRight.Phrase = new Phrase("");
                //    agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                //    cellIdentityContentRight.Phrase = new Phrase("");
                //    agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                //}
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Country, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Contact, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                PdfPCell agentCellIdentityOpeningLetter = new PdfPCell(agentIdentityOpeningLetter); // dont remove
                agentIdentityOpeningLetter.ExtendLastRow = false;
                agentIdentityOpeningLetter.SpacingAfter = 10f;
                document.Add(agentIdentityOpeningLetter);


                PdfPTable agentIdentityOpeningLetterHeader = new PdfPTable(1);
                bodyContentCenter.Phrase = new Phrase("COMMISSION AGREEMENT NO: " + viewModel.DispositionNumber, bold_font);
                agentIdentityOpeningLetterHeader.AddCell(bodyContentCenter);
                bodyContentCenter.Phrase = new Phrase("FOR SALES CONTRACT NO: " + viewModel.SalesContractNo, bold_font);
                agentIdentityOpeningLetterHeader.AddCell(bodyContentCenter);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetterHeader.AddCell(cellIdentityContentRight);
                PdfPCell agentIdentityOpeningLetterHeaderCell = new PdfPCell(agentIdentityOpeningLetterHeader); // dont remove
                agentIdentityOpeningLetterHeader.ExtendLastRow = false;
                agentIdentityOpeningLetterHeader.SpacingAfter = 10f;
                document.Add(agentIdentityOpeningLetterHeader);

                #endregion

                #region agentBody
                string agentFirstParagraphString = "This is to confirm that your order for " + viewModel.Buyer.Name + " concerning " + viewModel.OrderQuantity + " ( " + QuantityToText + ") " + uom + " of ";
                Paragraph agentFirstParagraph = new Paragraph(agentFirstParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                document.Add(agentFirstParagraph);
                string agentFirstParagraphStringName = viewModel.Commodity.Name;
                Paragraph agentFirstParagraphName = new Paragraph(agentFirstParagraphStringName, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                document.Add(agentFirstParagraphName);
                string agentFirstParagraphStringDescription = viewModel.CommodityDescription;
                Paragraph agentFirstParagraphDescription = new Paragraph(agentFirstParagraphStringDescription, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                document.Add(agentFirstParagraphDescription);
                string agentFirstParagraphStringContruction = "CONSTRUCTION: " + viewModel.Material.Name + viewModel.MaterialConstruction.Name + " / " + viewModel.YarnMaterial.Name + " WIDTH: " + viewModel.MaterialWidth;
                Paragraph agentFirstParagraphContruction = new Paragraph(agentFirstParagraphStringContruction, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentFirstParagraphContruction.SpacingAfter = 10f;
                document.Add(agentFirstParagraphContruction);
                string agentSecondParagraphString = "Placed with us, P.T. DAN LIRIS - SOLO INDONESIA, is inclusive of " + viewModel.Commission + " sales commission each " + uom1 + " on " + viewModel.TermOfShipment + " value, payable to you upon final negotiation and clearance of " + viewModel.TermOfPayment.Name + '.';
                Paragraph agentSecondParagraph = new Paragraph(agentSecondParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentSecondParagraph.SpacingAfter = 10f;
                document.Add(agentSecondParagraph);
                string agentThirdParagraphString = "Kindly acknowledge receipt by undersigning this Commission Agreement letter and returned one copy to us after having been confirmed and signed by you.";
                Paragraph agentThirdParagraph = new Paragraph(agentThirdParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentThirdParagraph.SpacingAfter = 30f;
                document.Add(agentThirdParagraph);
                #endregion

                #region signature
                PdfPTable signatureAgent = new PdfPTable(2);
                signatureAgent.SetWidths(new float[] { 1f, 1f });
                signatureAgent.SetWidths(new float[] { 1f, 1f });
                cell_signature.Phrase = new Phrase("Accepted and confrmed :", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("PT DANLIRIS", normal_font);
                signatureAgent.AddCell(cell_signature);

                cell_signature.Phrase = new Phrase("", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("", normal_font);
                signatureAgent.AddCell(cell_signature);

                string signatureAreaAgent = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    signatureAreaAgent += Environment.NewLine;
                }

                cell_signature.Phrase = new Phrase(signatureArea, normal_font);
                signatureAgent.AddCell(cell_signature);
                signatureAgent.AddCell(cell_signature);

                cell_signature.Phrase = new Phrase("(...........................)", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("ROBBY OS", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("Authorized signature", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("Marketing Manager", normal_font);
                signatureAgent.AddCell(cell_signature);
                cellIdentityContentRight.Phrase = new Phrase("");
                signatureAgent.AddCell(cellIdentityContentRight);

                PdfPCell signatureCellAgent = new PdfPCell(signatureAgent); // dont remove
                signatureAgent.ExtendLastRow = false;
                signatureAgent.SpacingAfter = 10f;
                document.Add(signatureAgent);
            }
            #endregion

            #endregion



            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        class FPSalesContractExportWithHeaderPDFTemplatePageEvent : iTextSharp.text.pdf.PdfPageEventHelper
        {
            private FinishingPrintingSalesContractViewModel viewModel;
            private int timeoffset;
            private Image borderImage;

            public FPSalesContractExportWithHeaderPDFTemplatePageEvent(FinishingPrintingSalesContractViewModel viewModel, int timeoffset)
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
                imageDL.SetAbsolutePosition(marginLeft, height - imageDL.ScaledHeight - marginTop + 110);
                cb.AddImage(imageDL, inlineImage: true);

                #endregion

                #region ADDRESS

                var headOfficeX = width / 2 + 30;
                var headOfficeY = height - marginTop + 45;

                var branchOfficeY = height - marginTop + 95;

                byte[] imageByte = Convert.FromBase64String(Base64ImageStrings.LOGO_NAME);
                Image image1 = Image.GetInstance(imageByte);
                if (image1.Width > 100)
                {
                    float percentage = 0.0f;
                    percentage = 100 / image1.Width;
                    image1.ScalePercent(percentage * 100);
                }
                image1.SetAbsolutePosition(marginLeft + 80, height - image1.ScaledHeight - marginTop + 115);
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
                imageIso.SetAbsolutePosition(width - imageIso.ScaledWidth - marginRight, height - imageIso.ScaledHeight - marginTop + 110);
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
                borderImage.ScaleAbsolute(pageWidth, borderHeight - 105);
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
