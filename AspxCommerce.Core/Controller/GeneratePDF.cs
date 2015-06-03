using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;



namespace AspxCommerce.Core
{
    public class GeneratePDF
    {

        public void GenerateOrderDetailsPDF(string tableContent, string hdnDescriptionValue, string TemplateName, int storeID, int portalID, string cultureName)
        {
            var detailDataObj = JSONHelper.Deserialise<List<OrderDetailsData>>(tableContent);

            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition",
                                                   "attachment;filename=" + "MyReport_" +
                                                   DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".pdf");

            Document doc = new Document(iTextSharp.text.PageSize.A4, 0, 0, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);
            writer.PageEvent = new MyPageEventHandler(" ");

            doc.Open();

            //--- start of header----
            PdfPTable headerTbl = new PdfPTable(2);
            headerTbl.SetWidths(new int[2] {10, 15});

            headerTbl.TotalWidth = doc.PageSize.Width;
            //StoreSettingInfo ss = new StoreSettingInfo();
            StoreSettingConfig ssc = new StoreSettingConfig();
            string storeLogoUrl = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL,storeID,portalID,cultureName);
            headerTbl.TotalWidth = doc.PageSize.Width;
            iTextSharp.text.Image logo =
                iTextSharp.text.Image.GetInstance(
                    HttpContext.Current.Request.MapPath("~/" + storeLogoUrl));
            logo.ScalePercent(50f);
            PdfPCell cellH = new PdfPCell(logo);
            cellH.AddElement(logo);
            cellH.HorizontalAlignment = Element.ALIGN_LEFT;
            cellH.PaddingLeft = 5;

            cellH.Border = Rectangle.NO_BORDER;
            // cellH.Border = Rectangle.BOTTOM_BORDER;//for underline below logo
            headerTbl.AddCell(cellH);

            //--for second cell ----------
            Paragraph pa = new Paragraph("Order Details",
                                         FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, Font.BOLD,
                                                             new BaseColor(0, 0, 255)));
            PdfPCell cell2 = new PdfPCell(pa);
            pa.Alignment = Element.ALIGN_BOTTOM;
            cell2.AddElement(pa);
            //cell2.HorizontalAlignment = Element.ALIGN_BASELINE;
            cell2.Border = Rectangle.NO_BORDER;
            //  cell2.Border = Rectangle.BOTTOM_BORDER;// for underline below logo
            headerTbl.AddCell(cell2);

            //for one line spacing
            PdfPCell blankCell = new PdfPCell();
            blankCell.Colspan = 2;
            blankCell.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankCell);
            var headingCellBackColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F4F8FC"));

            //for header description
            var r1 = JSONHelper.Deserialise<OrderDetailsData>(hdnDescriptionValue);
            string orderedDate = r1.OrderDate;
            string storeName = r1.StoreName;
            string storeDescription = r1.StoreDescription;
            string paymentGatewayType = r1.PaymentGatewayType;
            string paymentMethod = r1.PaymentMethod;
            string billingAddress = r1.BillingAddress;
            billingAddress = billingAddress.Replace("<b>", "").Replace("<B>", "");
            billingAddress = billingAddress.Trim();
            billingAddress = billingAddress.Replace("</b>", "").Replace("</B>", "").Replace("<BR>", Environment.NewLine).Replace("<br>", Environment.NewLine);
            string[] splitter = new string[] {"<br>"};
            string[] billingAddColl = billingAddress.Split(splitter, StringSplitOptions.None);

            Paragraph bodyPa =
                new Paragraph(
                    "Ordered Date: " + orderedDate + Environment.NewLine + "Store Name: " + storeName +
                    Environment.NewLine + "Store Descrption: " + storeDescription +
                    Environment.NewLine + "Payment Gateway Type: " + paymentGatewayType + Environment.NewLine +
                    "Payment Method: " + paymentMethod,
                    FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.NORMAL, new BaseColor(0, 0, 255)));

            PdfPCell bodyCell = new PdfPCell(bodyPa);
            bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
            bodyCell.PaddingLeft = 5;
            bodyCell.Border = Rectangle.NO_BORDER;

            bodyCell.HorizontalAlignment = Element.PARAGRAPH;
            bodyCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(bodyCell);

            string billAdd = string.Empty;

            for (int i = 0; i <= billingAddColl.Length - 1; i++)
            {
                if (billingAddColl[i] != "")
                {
                    if (billingAddColl[i].ToLower().Trim() == "billing address:")
                    {
                        billAdd += billingAddColl[i].ToUpper() + Environment.NewLine;
                    }
                    else
                    {
                        billAdd += billingAddColl[i].Trim() + Environment.NewLine;
                    }
                }
            }
            PdfPCell billingCell =
                new PdfPCell(new Paragraph(billAdd,
                                           FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.NORMAL,
                                                               new BaseColor(0, 0, 225))));
            billingCell.Border = Rectangle.NO_BORDER;
            billingCell.PaddingLeft = 50f;
            billingCell.PaddingRight = 20;
            billingCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(billingCell);

            //for one line spacing
            PdfPCell blankCell2 = new PdfPCell();
            blankCell2.Colspan = 2;
            blankCell2.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankCell2);
            headerTbl.SpacingAfter = 10f;
            doc.Add(headerTbl);

            //---end of header

            //--- Detail parts---
            PdfPTable bodyTbl = new PdfPTable(7);
            var headingFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 225));
            var bodyFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 0));
            PdfPCell headingCell =
                new PdfPCell(new Paragraph("Ordered Items:",
                                           FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL,
                                                               new BaseColor(0, 0, 225))));
            headingCell.Colspan = 7;
            headingCell.Border = Rectangle.NO_BORDER;
            bodyTbl.AddCell(headingCell);

            // bodyTbl.TotalWidth = doc.PageSize.Width-100;
            var cellHeadingColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#E3EDFA"));

            Paragraph middlePart = new Paragraph("Item Name", headingFont);
            PdfPCell middleCell = new PdfPCell(middlePart);
            middleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            middleCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(middleCell);

            var shippingMethodCell = new PdfPCell(new Paragraph("Shipping Method", headingFont));
            shippingMethodCell.HorizontalAlignment = Element.ALIGN_CENTER;
            shippingMethodCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(shippingMethodCell);

            var shippingAddressHeadingCell = new PdfPCell(new Paragraph("Shipping Address", headingFont));
            shippingAddressHeadingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            shippingAddressHeadingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(shippingAddressHeadingCell);

            var shippingRateHeadingCell = new PdfPCell(new Paragraph("Shipping Rate", headingFont));
            shippingRateHeadingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            shippingRateHeadingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(shippingRateHeadingCell);

            var priceHedingCell = new PdfPCell(new Paragraph("Price", headingFont));
            priceHedingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            priceHedingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(priceHedingCell);

            var quantityHeadingCell = new PdfPCell(new Paragraph("Quantity", headingFont));
            quantityHeadingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            quantityHeadingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(quantityHeadingCell);

            var subTotalHeadingCell = new PdfPCell(new Paragraph("SubTotal", headingFont));
            subTotalHeadingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            subTotalHeadingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(subTotalHeadingCell);

            int length = detailDataObj.Count;
            for (int j = 0; j <= length - 1; j++)
            {
                var itemNameCell = new PdfPCell(new Paragraph(detailDataObj[j].ItemName, bodyFont));
                itemNameCell.PaddingLeft = 15;
                if (detailDataObj[j].ItemName == "")
                {
                    itemNameCell.Border = Rectangle.LEFT_BORDER;
                    if (j == length - 1)
                    {
                        itemNameCell.Border = Rectangle.LEFT_BORDER + Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(itemNameCell);

                var shippingMethodNameCell = new PdfPCell(new Paragraph(detailDataObj[j].ShippingMethodName, bodyFont));
                shippingMethodNameCell.PaddingLeft = 5;
                if (detailDataObj[j].ShippingMethodName == "")
                {
                    shippingMethodNameCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        shippingMethodNameCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(shippingMethodNameCell);

                var shippingAddressCell = new PdfPCell(new Paragraph(detailDataObj[j].ShippingAddress, bodyFont));
                shippingAddressCell.PaddingLeft = 5;
                if (detailDataObj[j].ShippingAddress == "")
                {
                    shippingAddressCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        shippingAddressCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(shippingAddressCell);

                var shippingRateCell = new PdfPCell(new Paragraph(detailDataObj[j].ShippingRate, bodyFont));
                shippingRateCell.PaddingRight = 15;
                shippingRateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (detailDataObj[j].ShippingRate == "")
                {
                    shippingRateCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        shippingRateCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(shippingRateCell);

                var priceCell = new PdfPCell(new Paragraph(detailDataObj[j].Price, bodyFont));
                priceCell.PaddingRight = 15;
                priceCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (detailDataObj[j].Price == "")
                {
                    priceCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        priceCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(priceCell);

                var quantityCell = new PdfPCell(new Paragraph(detailDataObj[j].Quantity, bodyFont));
                quantityCell.PaddingLeft = 15;
                if (detailDataObj[j].Quantity == "Tax Total:" || detailDataObj[j].Quantity == "Shipping Cost:" ||
                    detailDataObj[j].Quantity == "Discount Amount:" || detailDataObj[j].Quantity == "Coupon Amount:" ||
                    detailDataObj[j].Quantity == "Grand Total:")
                {
                    quantityCell.PaddingLeft = 3;
                }
                else
                {
                    quantityCell.PaddingLeft = 3; 
                }
                bodyTbl.AddCell(quantityCell);

                var subTotalCell = new PdfPCell(new Paragraph(detailDataObj[j].SubTotal, bodyFont));
                subTotalCell.PaddingRight = 15;
                subTotalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTbl.AddCell(subTotalCell);
            }
            doc.Add(bodyTbl);

            // StyleSheet styles = new StyleSheet();
            // HTMLWorker hw = new HTMLWorker(doc);
            //hw.Parse(new StringReader(tableContent));
            doc.Close();
        }

        public void GenerateInvoicePDF(string headerDetail, string tableContent, string hdnRemarksData, string hdnIsMultipleShipping, string TemplateName, int storeID, int portalID, string cultureName)
        {
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition",
                                                   "attachment;filename=" + "MyReport_Invoice_" +
                                                   DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".pdf");

            Document doc = new Document(iTextSharp.text.PageSize.A4, 0, 0, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);
            writer.PageEvent = new MyPageEventHandler(" ");
            doc.Open();

            //--- start of header----
            PdfPTable headerTbl = new PdfPTable(2);
            headerTbl.SetWidths(new int[2] {10, 15});
            //headerTbl.DefaultCell.Border = Rectangle.NO_BORDER;
            headerTbl.TotalWidth = doc.PageSize.Width;
            StoreSettingConfig ssc = new StoreSettingConfig();
            string storeLogoUrl = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, storeID, portalID, cultureName);
            string logoPath= HttpContext.Current.Request.MapPath("~/" + storeLogoUrl);
            if (File.Exists(logoPath))
            {
                //iTextSharp.text.Image logo =
                //    iTextSharp.text.Image.GetInstance(
                //        HttpContext.Current.Request.MapPath("~/" + storeLogoUrl));
                iTextSharp.text.Image logo =
                    iTextSharp.text.Image.GetInstance(logoPath.Replace("uploads", "uploads/Small").Replace("\\", @"//"));
                logo.ScalePercent(50f);
                PdfPCell cellH = new PdfPCell(logo);
                cellH.AddElement(logo);

                cellH.HorizontalAlignment = Element.ALIGN_LEFT;
                cellH.PaddingLeft = 5;
                cellH.Border = Rectangle.NO_BORDER;
                // cellH.Border = Rectangle.BOTTOM_BORDER;// for underline below logo
                headerTbl.AddCell(cellH);
            }
            //--for second cell ----------
            Paragraph pa = new Paragraph("Invoice",
                                         FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, Font.BOLD,
                                                             new BaseColor(0, 0, 255)));
            PdfPCell cell2 = new PdfPCell(pa);
            pa.Alignment = Element.ALIGN_BOTTOM;
            cell2.AddElement(pa);

            cell2.Border = Rectangle.NO_BORDER;
            // cell2.Border = Rectangle.BOTTOM_BORDER;// for underline below logo
            headerTbl.AddCell(cell2);

            //for one line spacing
            PdfPCell blankCell = new PdfPCell();
            blankCell.Colspan = 2;
            blankCell.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankCell);

            var headingCellBackColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F4F8FC"));
            var headingFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.NORMAL, new BaseColor(0, 0, 255));
            var textFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 255));
            //for header description
            var h1 = JSONHelper.Deserialise<InvoiceDetailDataTableInfo>(headerDetail);

            //Paragraph headingPara =
            //    new Paragraph("Invoice No: " + h1.InvoiceNo + Environment.NewLine + "Invoice Date: " + h1.InvoiceDate, headingFont);
            Paragraph headingPara =
                new Paragraph("Invoice No: " + h1.InvoiceNo, headingFont);
            PdfPCell invoiceDateCell = new PdfPCell(headingPara);
            invoiceDateCell.Colspan = 2;
            invoiceDateCell.Border = Rectangle.NO_BORDER;
            invoiceDateCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(invoiceDateCell);

            //for one line spacing
            PdfPCell blankCell2 = new PdfPCell();
            blankCell2.Colspan = 2;
            blankCell2.Border = Rectangle.NO_BORDER;
            blankCell2.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(blankCell2);


            Paragraph invoiceStorePara =
                new Paragraph(
                    "Store Name: " + h1.StoreName + Environment.NewLine + "Store Description: " + h1.StoreDescription +
                    Environment.NewLine +
                    "Customer Name: " + h1.CustomerName + Environment.NewLine + "Customer Email: " + h1.CustomerEmail,
                    headingFont);
            PdfPCell invoiceStoreDetail = new PdfPCell(invoiceStorePara);
            invoiceStoreDetail.Border = Rectangle.NO_BORDER;
            invoiceStoreDetail.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(invoiceStoreDetail);

            Paragraph invoiceOrderDetailPara =
                new Paragraph(
                    "Order ID: " + h1.OrderId + Environment.NewLine + "Order Date: " + h1.OrderDate +
                    Environment.NewLine +
                    "Status: " + h1.Status +
                    Environment.NewLine + "Payment Method: " + h1.PaymentMethod + Environment.NewLine +
                    "Shipping Method: " + h1.ShippingMethod, headingFont);
            PdfPCell invoiceOrderDetail = new PdfPCell(invoiceOrderDetailPara);
            invoiceOrderDetail.Border = Rectangle.NO_BORDER;
            invoiceOrderDetail.PaddingLeft = 50f;
            invoiceOrderDetail.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(invoiceOrderDetail);

            //for one line spacing
            PdfPCell blankCell3 = new PdfPCell();
            blankCell3.Colspan = 2;
            blankCell3.Border = Rectangle.NO_BORDER;
            blankCell3.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(blankCell3);
            var shippingDetail = JSONHelper.Deserialise<InvoiceDetailByorderIDInfo>(h1.ShippingAddress);

            string shipAdd = string.Empty;
            if (!string.IsNullOrEmpty(shippingDetail.ShippingName))
                shipAdd += shippingDetail.ShippingName.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(shippingDetail.ShipCompany))
                shipAdd += shippingDetail.ShipCompany.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(shippingDetail.ShipAddress1))
                shipAdd += shippingDetail.ShipAddress1.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(shippingDetail.ShipAddress2))
                shipAdd += shippingDetail.ShipAddress2.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(shippingDetail.ShipCity))
                shipAdd += shippingDetail.ShipCity.Trim();
            if (!string.IsNullOrEmpty(shippingDetail.ShipState))
                shipAdd += ", " + shippingDetail.ShipState.Trim();
            if (!string.IsNullOrEmpty(shippingDetail.ShipZip))
                shipAdd += ", " + shippingDetail.ShipZip.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(shippingDetail.ShipCountry))
                shipAdd += shippingDetail.ShipCountry.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(shippingDetail.ShipEmail))
                shipAdd += shippingDetail.ShipEmail.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(shippingDetail.ShipPhone))
                shipAdd += shippingDetail.ShipPhone.Trim();
            if (!string.IsNullOrEmpty(shippingDetail.ShipMobile))
                shipAdd += ", " + shippingDetail.ShipMobile.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(shippingDetail.ShipFax))
                shipAdd += shippingDetail.ShipFax.Trim() + Environment.NewLine;
            if (!String.IsNullOrEmpty(shippingDetail.ShipWebsite))
                shipAdd += shippingDetail.ShipWebsite.Trim();

            PdfPCell shippingAddCell = new PdfPCell();
            PdfPTable sTable = new PdfPTable(1);

            sTable.WidthPercentage = 100;
            PdfPCell sHCell = new PdfPCell(new Paragraph("Shipping Address:" + Environment.NewLine, headingFont));
            sHCell.Border = Rectangle.NO_BORDER;
            if (hdnIsMultipleShipping != "true")
            {
                sTable.AddCell(sHCell);
            }
            PdfPCell scell = new PdfPCell(new Paragraph(shipAdd, textFont));
            scell.Border = Rectangle.NO_BORDER;
            scell.PaddingLeft = 30;
            sTable.AddCell(scell);
            shippingAddCell.AddElement(sTable);
            shippingAddCell.Border = Rectangle.NO_BORDER;
            shippingAddCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(shippingAddCell);

            var billingDetail = JSONHelper.Deserialise<InvoiceDetailByorderIDInfo>(h1.BillingAddress);

            string billAdd = string.Empty;
            if (!string.IsNullOrEmpty(billingDetail.BillingName))
                billAdd += billingDetail.BillingName.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.Company))
                billAdd += billingDetail.Company.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.Address1))
                billAdd += billingDetail.Address1.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.Address2))
                billAdd += billingDetail.Address2.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.City))
                billAdd += billingDetail.City.Trim();
            if (!string.IsNullOrEmpty(billingDetail.State))
                billAdd += ", " + billingDetail.State.Trim();
            if (!string.IsNullOrEmpty(billingDetail.Zip))
                billAdd += ", " + billingDetail.Zip.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.Country))
                billAdd += billingDetail.Country.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.Email))
                billAdd += billingDetail.Email.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.Phone))
                billAdd += billingDetail.Phone.Trim();
            if (!string.IsNullOrEmpty(billingDetail.Mobile))
                billAdd += ", " + billingDetail.Mobile.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.Fax))
                billAdd += billingDetail.Fax.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(billingDetail.Website))
                billAdd += billingDetail.Website.Trim();

            PdfPCell billingAddCell = new PdfPCell();
            PdfPTable bTable = new PdfPTable(1);

            bTable.WidthPercentage = 100;
            PdfPCell bHCell = new PdfPCell(new Paragraph("Billing Address:" + Environment.NewLine, headingFont));
            bHCell.Border = Rectangle.NO_BORDER;
            bTable.AddCell(bHCell);

            PdfPCell bcell = new PdfPCell(new Paragraph(billAdd, textFont));
            bcell.Border = Rectangle.NO_BORDER;
            bcell.PaddingLeft = 30;
            bTable.AddCell(bcell);
            billingAddCell.AddElement(bTable);

            billingAddCell.Border = Rectangle.NO_BORDER;
            billingAddCell.PaddingLeft = 50f;
            billingAddCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(billingAddCell);

            //for one line spacing
            PdfPCell blankCell4 = new PdfPCell();
            blankCell4.Colspan = 2;
            blankCell4.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankCell4);
            headerTbl.SpacingAfter = 10f;
            doc.Add(headerTbl);

            //headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 10), writer.DirectContent);
            //---end of header
            var bodyFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 0));
            var cellHeadingColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#E3EDFA"));

            var invoiceDetailDataObj = JSONHelper.Deserialise<List<InvoiceDetailDataTableInfo>>(tableContent);
            PdfPTable bodyTbl1;
            string isMultipleShipping = hdnIsMultipleShipping;
            int noOfCol;
            if (isMultipleShipping == "true")
            {
                PdfPTable bodyTbl = new PdfPTable(8);
                PdfPCell headingCell =
                    new PdfPCell(new Paragraph("Ordered Items:",
                                               FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL,
                                                                   new BaseColor(0, 0, 225))));
                float[] widths = new float[] {25f, 50f, 50f, 45f, 70f, 35f, 60f, 30f};
                bodyTbl.SetWidths(widths);
                headingCell.Colspan = 8;
                headingCell.Border = Rectangle.NO_BORDER;
                bodyTbl.AddCell(headingCell);
                noOfCol = 8;
                bodyTbl1 = bodyTbl;
            }
            else
            {
                PdfPTable bodyTbl = new PdfPTable(6);
                PdfPCell headingCell =
                    new PdfPCell(new Paragraph("Ordered Items:",
                                               FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL,
                                                                   new BaseColor(0, 0, 225))));
                float[] widths = new float[] {25f, 50f, 50f, 35f, 60f, 30f};
                bodyTbl.SetWidths(widths);
                headingCell.Colspan = 6;
                headingCell.Border = Rectangle.NO_BORDER;
                bodyTbl.AddCell(headingCell);
                noOfCol = 6;
                bodyTbl1 = bodyTbl;
            }
            int length = invoiceDetailDataObj.Count;
            for (int j = 0; j <= length - 1; j++)
            {
                var itemIdCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].ItemId, bodyFont));
                itemIdCell.PaddingLeft = 10;
                if (j == 0)
                {
                    itemIdCell.BackgroundColor = cellHeadingColor;
                    itemIdCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                if (invoiceDetailDataObj[j].ItemName == "")
                {
                    itemIdCell.Border = Rectangle.LEFT_BORDER;
                    if (j == length - 1)
                    {
                        itemIdCell.Border = Rectangle.LEFT_BORDER + Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl1.AddCell(itemIdCell);

                var itemSkuCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].SKU, bodyFont));
                itemSkuCell.PaddingLeft = 15;
                if (j == 0)
                {
                    itemSkuCell.BackgroundColor = cellHeadingColor;
                    itemSkuCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                if (invoiceDetailDataObj[j].SKU == "")
                {
                    itemSkuCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        itemSkuCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl1.AddCell(itemSkuCell);



                var itemNameCell = new PdfPCell();
                PdfPTable imageTbl = new PdfPTable(1); // table for adding item image with item name

                var itemNameCellImg = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].ItemName, bodyFont));//add item name
                itemNameCellImg.PaddingLeft = 10;
                itemNameCellImg.Border = Rectangle.NO_BORDER;
                if (j == 0)
                {
                    itemNameCell.BackgroundColor = cellHeadingColor;
                    itemNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                if (invoiceDetailDataObj[j].ItemName == "")
                {
                    itemNameCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        itemNameCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                else
                {
                    imageTbl.WidthPercentage = 100;
                    if (invoiceDetailDataObj[j].ImagePath != null && invoiceDetailDataObj[j].ImagePath.IndexOf("Module")>0) // add image if ImagePath is not null
                    {
                        string path =
                            invoiceDetailDataObj[j].ImagePath.Substring(
                                int.Parse(invoiceDetailDataObj[j].ImagePath.IndexOf("Module").ToString()));
                        string phyPath = HttpContext.Current.Request.MapPath("~/" + path);
                        if (File.Exists(phyPath))
                        {
                            Image ImagePath =
                                Image.GetInstance(phyPath.Replace("\\", @"//"));
                            ImagePath.ScaleToFit(80f, 58f);
                            ImagePath.ScaleAbsoluteWidth(58f);
                            PdfPCell itemImageCell = new PdfPCell(ImagePath);
                            itemImageCell.Border = Rectangle.NO_BORDER;
                            itemImageCell.PaddingLeft = 3f;
                            itemImageCell.AddElement(ImagePath);

                        
                        imageTbl.AddCell(itemImageCell);
                    }
                }
                    imageTbl.AddCell(itemNameCellImg);
                }
                itemNameCell.AddElement(imageTbl);
                bodyTbl1.AddCell(itemNameCell);

                if (invoiceDetailDataObj[j].ShippingMethodName != null)
                {
                    var shippingMethodNameCell =
                        new PdfPCell(new Paragraph(invoiceDetailDataObj[j].ShippingMethodName, bodyFont));
                    shippingMethodNameCell.PaddingLeft = 5f;
                    if (j == 0)
                    {
                        shippingMethodNameCell.BackgroundColor = cellHeadingColor;
                        shippingMethodNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    if (invoiceDetailDataObj[j].ShippingMethodName == "" && invoiceDetailDataObj[j].ItemName == "")
                    {
                        shippingMethodNameCell.Border = Rectangle.NO_BORDER;
                        if (j == length - 1)
                        {
                            shippingMethodNameCell.Border = Rectangle.BOTTOM_BORDER;
                        }
                    }
                    bodyTbl1.AddCell(shippingMethodNameCell);
                }
                if (invoiceDetailDataObj[j].ShippingAddressDetail != null)
                {
                    var shippingAddressCell = invoiceDetailDataObj[j].ShippingAddressDetail;
                    shippingAddressCell = shippingAddressCell.Replace("<BR>", Environment.NewLine).Replace("<br>",
                                                                                                           Environment.
                                                                                                               NewLine);

                    var shippingAddressDetailCell = new PdfPCell(new Paragraph(shippingAddressCell, bodyFont));
                    shippingAddressDetailCell.PaddingLeft = 5f;
                    if (j == 0)
                    {
                        shippingAddressDetailCell.BackgroundColor = cellHeadingColor;
                        shippingAddressDetailCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    if (invoiceDetailDataObj[j].ShippingAddressDetail == "")
                    {
                        shippingAddressDetailCell.Border = Rectangle.NO_BORDER;
                        if (j == length - 1)
                        {
                            shippingAddressDetailCell.Border = Rectangle.BOTTOM_BORDER;
                        }
                    }
                    bodyTbl1.AddCell(shippingAddressDetailCell);
                }

                var quantityCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].Quantity, bodyFont));
                quantityCell.PaddingRight = 30f;
                quantityCell.HorizontalAlignment = Element.ALIGN_CENTER;
                if (j == 0)
                {
                    quantityCell.BackgroundColor = cellHeadingColor;
                    quantityCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                if (invoiceDetailDataObj[j].Quantity == "")
                {
                    quantityCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        quantityCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl1.AddCell(quantityCell);

                var priceCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].Price, bodyFont));
                priceCell.PaddingRight = 30f;
                priceCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (j == 0)
                {
                    priceCell.BackgroundColor = cellHeadingColor;
                    priceCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                if (invoiceDetailDataObj[j].Price == "Sub Total:" || invoiceDetailDataObj[j].Price == "Taxes:" ||
                    invoiceDetailDataObj[j].Price == "Shipping Cost:" ||
                    invoiceDetailDataObj[j].Price == "Discount Amount:" ||
                    invoiceDetailDataObj[j].Price == "Coupon Amount:" ||
                    invoiceDetailDataObj[j].Price == "Grand Total:" ||
                    invoiceDetailDataObj[j].Price.ToLower() == "item tax:")
                {
                    priceCell.PaddingLeft = 15f;
                    priceCell.HorizontalAlignment = Element.ALIGN_LEFT;
                }
                else
                {
                    priceCell.PaddingLeft = 15f;
                    priceCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                }
                bodyTbl1.AddCell(priceCell);

                var subTotalCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].SubTotal, bodyFont));
                subTotalCell.PaddingRight = 15f;
                subTotalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (j == 0)
                {
                    subTotalCell.BackgroundColor = cellHeadingColor;
                    subTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                bodyTbl1.AddCell(subTotalCell);
            }

            PdfPCell remarekCell = new PdfPCell(new Paragraph(hdnRemarksData, bodyFont));
            remarekCell.Border = Rectangle.NO_BORDER;
            remarekCell.Colspan = noOfCol;
            bodyTbl1.AddCell(remarekCell);
            doc.Add(bodyTbl1);

            //iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
            // iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
            //hw.Parse(new StringReader(tableContent));
            doc.Close();
        }

        public void CreateShipmentLabel(AddressInfo destinationAddress, WareHouseAddress wareHouseAddress, AspxCommonInfo aspxCommonObj, BasicPackageInfo basicPackageInfo)
        {
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition",
                                                   "attachment;filename=" + "ShipmentLabel_" +
                                                   DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".pdf");

            Document doc = new Document(iTextSharp.text.PageSize.A4, 0, 0, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);

            writer.PageEvent = new MyPageEventHandler(basicPackageInfo.CautionMessage);
            doc.Open();
            var textFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 255));

            PdfPTable shipmentTalbe = new PdfPTable(2);
            shipmentTalbe.SetWidths(new int[2] {5, 15});
             
            //string cautionMessage = basicPackageInfo.CautionMessage;
            //PdfPCell verticalC = new PdfPCell(); //new PdfPCell(new Paragraph(cautionMessage, textFont));
            //verticalC.Rotation = 90;
            //// verticalC.VerticalAlignment = Element.ALIGN_CENTER;
            //verticalC.PaddingTop = 30f;
            //verticalC.PaddingBottom = 50f;
            //verticalC.Border = Rectangle.NO_BORDER;
            ////  shipmentTalbe.AddCell("");
            //shipmentTalbe.AddCell(verticalC);


            PdfPTable headerTbl = new PdfPTable(2);
            headerTbl.SetWidths(new int[2] {10, 15});
            float[] widths = new float[] { 200f,300f };
            // headerTbl.TotalWidth = doc.PageSize.Width;
            headerTbl.WidthPercentage = 100;

            Paragraph pa = new Paragraph("From",
                                         FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, Font.BOLD,
                                                             new BaseColor(0, 0, 255)));

            PdfPCell cell = new PdfPCell(pa);
            cell.PaddingLeft = 15f;
            //   pa.Alignment = Element.ALIGN_BOTTOM;
            cell.Border = Rectangle.NO_BORDER + Rectangle.RIGHT_BORDER;
            cell.AddElement(pa);

            PdfPTable fromTable = new PdfPTable(1);
            fromTable.WidthPercentage = 100;

            string fromAdd = string.Empty;
            if (!string.IsNullOrEmpty(wareHouseAddress.Name))
                fromAdd += wareHouseAddress.Name.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(wareHouseAddress.Address))
                fromAdd += wareHouseAddress.Address.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(wareHouseAddress.StreetAddress1))
                fromAdd += wareHouseAddress.StreetAddress1.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(wareHouseAddress.StreetAddress2))
                fromAdd += wareHouseAddress.StreetAddress2.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(wareHouseAddress.City))
                fromAdd += wareHouseAddress.City.Trim();
            if (!string.IsNullOrEmpty(wareHouseAddress.State))
                fromAdd += ", " + wareHouseAddress.State.Trim();
            if (!string.IsNullOrEmpty(wareHouseAddress.PostalCode))
                fromAdd += ", " + wareHouseAddress.PostalCode.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(wareHouseAddress.Country))
                fromAdd += wareHouseAddress.Country.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(wareHouseAddress.Phone))
                fromAdd += wareHouseAddress.Phone.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(wareHouseAddress.Fax))
                fromAdd += wareHouseAddress.Fax.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(wareHouseAddress.Email))
                fromAdd += wareHouseAddress.Email.Trim() + Environment.NewLine;

            PdfPCell fromAddcell = new PdfPCell(new Paragraph(fromAdd, textFont));
            fromAddcell.Border = Rectangle.NO_BORDER;
            fromAddcell.PaddingLeft = 30f;
            fromTable.AddCell(fromAddcell);
            cell.AddElement(fromTable);
            headerTbl.AddCell(cell);


            Paragraph to = new Paragraph("To",
                                         FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, Font.BOLD,
                                                             new BaseColor(0, 0, 255)));
            PdfPCell cellTo = new PdfPCell(to);
            cellTo.PaddingLeft = 15f;
            cellTo.Border = Rectangle.NO_BORDER;
            cellTo.AddElement(to);
            PdfPTable toTable = new PdfPTable(1);
            toTable.WidthPercentage = 100;
            string toAddress = string.Empty;
            if (!string.IsNullOrEmpty(destinationAddress.FirstName))
                toAddress += destinationAddress.FirstName.Trim() + " ";
            if (!string.IsNullOrEmpty(destinationAddress.LastName))
                toAddress += destinationAddress.LastName.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.Company))
                toAddress += destinationAddress.Company.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.Address1))
                toAddress += destinationAddress.Address1.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.Address2))
                toAddress += destinationAddress.Address2.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.City))
                toAddress += destinationAddress.City.Trim();
            if (!string.IsNullOrEmpty(destinationAddress.State))
                toAddress += ", " + destinationAddress.State.Trim();
            if (!string.IsNullOrEmpty(destinationAddress.Zip))
                toAddress += ", " + destinationAddress.Zip.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.Country))
                toAddress += destinationAddress.Country.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.Email))
                toAddress += destinationAddress.Email.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.Phone))
                toAddress += destinationAddress.Phone.Trim();
            if (!string.IsNullOrEmpty(destinationAddress.Mobile))
                toAddress += ", " + destinationAddress.Mobile.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.Fax))
                toAddress += destinationAddress.Fax.Trim() + Environment.NewLine;
            if (!string.IsNullOrEmpty(destinationAddress.Website))
                toAddress += destinationAddress.Website.Trim();
            PdfPCell toAddresscell = new PdfPCell(new Paragraph(toAddress, textFont));
            toAddresscell.Border = Rectangle.NO_BORDER;
            toAddresscell.PaddingLeft = 30f;
            toTable.AddCell(toAddresscell);
            cellTo.AddElement(toTable);
            headerTbl.AddCell(cellTo);

            var cellBackColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F4F8FC"));

            // for one line spacing
            PdfPCell blankCell1 = new PdfPCell();
            blankCell1.Colspan = 2;
            blankCell1.Border = Rectangle.NO_BORDER;
            // blankCell1.BackgroundColor = cellBackColor;
            headerTbl.AddCell(blankCell1);

            // For Tracking Number
            //PdfPCell trackingCell = new PdfPCell();
            //trackingCell.Colspan = 2;
            //trackingCell.Border = Rectangle.NO_BORDER;
            //Paragraph tracingPa = new Paragraph("Tracking No: " + basicPackageInfo.TrackingNo + "",
            //                                    FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, Font.BOLD,
            //                                                        new BaseColor(0, 0, 255)));
            //trackingCell.AddElement(tracingPa);
            //headerTbl.AddCell(trackingCell);

            // For one line spacing after tracking no.

            PdfPCell blankCell2 = new PdfPCell();
            blankCell2.Colspan = 2;
            blankCell2.Border = Rectangle.NO_BORDER;
            // blankCell2.BackgroundColor = cellBackColor;
            headerTbl.AddCell(blankCell2);

            // for total weight

            PdfPCell blankMiddleCell = new PdfPCell();
            blankMiddleCell.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankMiddleCell);
            string dimension = basicPackageInfo.Length + "*" + basicPackageInfo.Width + "*" + basicPackageInfo.Height +
                               " " + basicPackageInfo.DimensionUnit;
            Paragraph wtPa = new Paragraph("Total Weight: " + basicPackageInfo.TotalWeight + "" + Environment.NewLine
                                           + "Weight Unit: " + basicPackageInfo.WeightUnit + "" + Environment.NewLine
                                           + "Dimension(L*W*H): " + dimension, textFont);
            PdfPCell wtCell = new PdfPCell();
            wtCell.AddElement(wtPa);
            wtCell.Border = Rectangle.NO_BORDER;
            wtCell.PaddingLeft = 30f;
            headerTbl.AddCell(wtCell);

            // for one line space after total weight

            PdfPCell blankCell3 = new PdfPCell();
            blankCell3.Colspan = 2;
            blankCell3.Border = Rectangle.NO_BORDER;
            // blankCell3.BackgroundColor = cellBackColor;
            headerTbl.AddCell(blankCell3);

            // for last row with logo

            Paragraph senderPa = new Paragraph("Sender Name: " + basicPackageInfo.SenderName + "" + Environment.NewLine
             + "Servce Type: " + basicPackageInfo.ServiceType, textFont);
            PdfPCell senderCell = new PdfPCell(senderPa);
            senderCell.Border = Rectangle.NO_BORDER + Rectangle.RIGHT_BORDER;
            headerTbl.AddCell(senderCell);

            StoreSettingConfig ssc = new StoreSettingConfig();
            string storeLogoUrl = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                            aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string logoPath = HttpContext.Current.Request.MapPath("~/" + storeLogoUrl);
            PdfPTable logoTable = new PdfPTable(1);
            if (File.Exists(logoPath))
            {
                iTextSharp.text.Image logo =
                                   iTextSharp.text.Image.GetInstance(logoPath.Replace("uploads", "uploads/Small").Replace("\\", @"//"));
                logo.ScalePercent(50f);
                PdfPCell logoImage = new PdfPCell(logo);
                logoImage.Border = Rectangle.NO_BORDER;
                logoImage.PaddingLeft = 30f;
                logoImage.AddElement(logo);
                //PdfPTable logoTable = new PdfPTable(1);
                logoTable.WidthPercentage = 100;
                logoTable.AddCell(logoImage);
            }
            var storeName = ssc.GetStoreSettingsByKey(StoreSetting.StoreName, aspxCommonObj.StoreID,
                                                      aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            PdfPCell storeNameCell = new PdfPCell(new Paragraph(storeName, textFont));
            storeNameCell.Border = Rectangle.NO_BORDER;
            storeNameCell.PaddingLeft = 30f;
            logoTable.AddCell(storeNameCell);

            PdfPCell logoCell = new PdfPCell();
            logoCell.AddElement(logoTable);
            logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
            logoCell.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(logoCell);

            headerTbl.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell detailCell = new PdfPCell();
            detailCell.Colspan = 2;
            detailCell.AddElement(headerTbl);
            shipmentTalbe.AddCell(detailCell);
            doc.Add(shipmentTalbe);
            doc.Close();

        }

        public class MyPageEventHandler : PdfPageEventHelper
        {
            private string watermarkText = string.Empty;

            public  MyPageEventHandler(string watermark)
            {
                watermarkText = watermark;
            }

            public override void OnStartPage(PdfWriter writer, Document document)
            {
                float fontSize = 30f;
                float xPosition = (document.PageSize.Width) - 180f;//150;
                float yPosition = (document.PageSize.Width) + 210f;//0f;//500
                float angle = 30;
                try
                {
                    PdfContentByte under = writer.DirectContentUnder;
                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
                    under.BeginText();
                    under.SetColorFill(BaseColor.LIGHT_GRAY);
                    under.SetFontAndSize(baseFont, fontSize);
                    under.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, watermarkText, xPosition, yPosition, angle);
                    under.EndText();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {

                var doc = document;
                var headerFooterFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.NORMAL,
                                                           new BaseColor(0, 0, 255));

                PdfPTable footerTbl = new PdfPTable(1);
                footerTbl.TotalWidth = doc.PageSize.Width;
                footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                DateTime dt = DateTime.Now;
                Paragraph para = new Paragraph("Copyright © " + dt.Year, headerFooterFont);
                PdfPCell cell = new PdfPCell(para);
                cell = new PdfPCell(para);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingRight = 15;
                footerTbl.AddCell(cell);
                footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin), writer.DirectContent);
            }
        }

        public class BasicPackageInfo
        {
            public string TrackingNo { get; set; }
            public decimal TotalWeight { get; set; }
            public string WeightUnit { get; set; }
            public string WaterMark { get; set; }
            public string BarcodeNo { get; set; }
            public string SenderName { get; set; }
            public string CautionMessage { get; set; }
            public string ServiceType { get; set; }
            public int Length { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string DimensionUnit { get; set; }
            public iTextSharp.text.PageSize PageSize { get; set; }
        }

        [DataContract]
        [Serializable]
        public class OrderDetailsData
        {
            [DataMember(Name = "orderDate")]
            public string OrderDate { get; set; }

            [DataMember(Name = "storeName")]
            public string StoreName { get; set; }

            [DataMember(Name = "storeDescription")]
            public string StoreDescription { get; set; }

            [DataMember(Name = "paymentGatewayType")]
            public string PaymentGatewayType { get; set; }

            [DataMember(Name = "paymentMethod")]
            public string PaymentMethod { get; set; }

            [DataMember(Name = "billingAddress")]
            public string BillingAddress { get; set; }

            [DataMember(Name = "itemName")]
            public string ItemName { get; set; }

            [DataMember(Name = "shippingMethodName")]
            public string ShippingMethodName { get; set; }

            [DataMember(Name = "shippingAddress")]
            public string ShippingAddress { get; set; }

            [DataMember(Name = "shippingRate")]
            public string ShippingRate { get; set; }

            [DataMember(Name = "price")]
            public string Price { get; set; }

            [DataMember(Name = "quantity")]
            public string Quantity { get; set; }

            [DataMember(Name = "subTotal")]
            public string SubTotal { get; set; }

        }

        [DataContract]
        [Serializable]
        public class InvoiceDetailDataTableInfo
        {
            [DataMember(Name = "InvoiceNo")]
            public string InvoiceNo { get; set; }

            [DataMember(Name = "InvoiceDate")]
            public string InvoiceDate { get; set; }

            [DataMember(Name = "StoreName")]
            public string StoreName { get; set; }

            [DataMember(Name = "StoreDescription")]
            public string StoreDescription { get; set; }

            [DataMember(Name = "CustomerName")]
            public string CustomerName { get; set; }

            [DataMember(Name = "CustomerEmail")]
            public string CustomerEmail { get; set; }

            [DataMember(Name = "OrderId")]
            public string OrderId { get; set; }

            [DataMember(Name = "OrderDate")]
            public string OrderDate { get; set; }

            [DataMember(Name = "Status")]
            public string Status { get; set; }

            [DataMember(Name = "PaymentMethod")]
            public string PaymentMethod { get; set; }

            [DataMember(Name = "ShippingMethod")]
            public string ShippingMethod { get; set; }

            [DataMember(Name = "ShippingAddress")]
            public string ShippingAddress { get; set; }

            [DataMember(Name = "BillingAddress")]
            public string BillingAddress { get; set; }

            [DataMember(Name = "ItemId")]
            public string ItemId { get; set; }

            [DataMember(Name = "ItemName")]
            public string ItemName { get; set; }

            [DataMember(Name = "SKU")]
            public string SKU { get; set; }

            [DataMember(Name = "ImagePath")]
            public string ImagePath { get; set; }

            [DataMember(Name = "ShippingMethodName")]
            public string ShippingMethodName { get; set; }

            [DataMember(Name = "ShippingAddressDetail")]
            public string ShippingAddressDetail { get; set; }

            [DataMember(Name = "Price")]
            public string Price { get; set; }

            [DataMember(Name = "Quantity")]
            public string Quantity { get; set; }

            [DataMember(Name = "SubTotal")]
            public string SubTotal { get; set; }

        }
    }
}
