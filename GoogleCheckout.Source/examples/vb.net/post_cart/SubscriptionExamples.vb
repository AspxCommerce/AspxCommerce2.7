'''''''''''''''''''''''''''''''''''''''''''''''''
 ' Copyright (C) 2011 Google Inc.
 '
 ' Licensed under the Apache License, Version 2.0 (the "License");
 ' you may not use this file except in compliance with the License.
 ' You may obtain a copy of the License at
 '
 '      http://www.apache.org/licenses/LICENSE-2.0
 '
 ' Unless required by applicable law or agreed to in writing, software
 ' distributed under the License is distributed on an "AS IS" BASIS,
 ' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 ' See the License for the specific language governing permissions and
 ' limitations under the License.
'''''''''''''''''''''''''''''''''''''''''''''''''/
'
' Edit History:
 '  4-21-2011   Joe Feser joe.feser@joefeser.com
 '  Initial Coding
 '  Place the code from one of these methods into your button click event
'

Imports GCheckout.Checkout
Imports GCheckout.Util

Module SubscriptionExamples

    Sub Main()
        InitialCharge()
        RecurringChargeRightAway()
    End Sub

    Sub InitialCharge()

        'http://code.google.com/apis/checkout/developer/Google_Checkout_Beta_Subscriptions.html
        'using an initial charge with a recurring charge using a different item.

        Dim cartRequest As CheckoutShoppingCartRequest _
                = New CheckoutShoppingCartRequest("123456", "merchantkey", _
                GCheckout.EnvironmentType.Sandbox, "USD", 120) 'GCheckoutButton1.CreateRequest()

        Dim initialItem As New ShoppingCartItem()
        Dim recurrentItem As New ShoppingCartItem()

        initialItem.Price = 0 'do not have an additional charge.
        initialItem.Quantity = 1
        initialItem.Name = "Item that shows in cart"
        initialItem.Description = "This is the item that shows in the cart"

        recurrentItem.Price = 2
        recurrentItem.Quantity = 1
        recurrentItem.Name = "Item that is charged every month"
        recurrentItem.Description = "Description for item that is charged every month"

        Dim subscription As New Subscription()
        subscription.Period = GCheckout.AutoGen.DatePeriod.MONTHLY
        subscription.Type = SubscriptionType.merchant

        Dim payment As New SubscriptionPayment()
        payment.MaximumCharge = 120
        payment.Times = 12

        subscription.AddSubscriptionPayment(payment)

        'You must set the item that will be charged for every month.
        subscription.RecurrentItem = recurrentItem

        'you must set the subscription for the item
        initialItem.Subscription = subscription

        cartRequest.AddItem(initialItem)

        'Debug.WriteLine(EncodeHelper.Utf8BytesToString(cartRequest.GetXml()))

        'Dim resp As GCheckout.Util.GCheckoutResponse = gg.Send()
        'Response.Redirect(resp.RedirectUrl, True)
    End Sub

    Sub RecurringChargeRightAway()
        Dim cartRequest As CheckoutShoppingCartRequest _
            = New CheckoutShoppingCartRequest("123456", "merchantkey", _
            GCheckout.EnvironmentType.Sandbox, "USD", 120) 'GCheckoutButton1.CreateRequest()

        Dim gSubscription As New Subscription
        Dim maxCharge As New SubscriptionPayment
        Dim urlDigitalItem As New DigitalItem(New  _
                                  Uri("http://www.url.com/login.aspx"), _
                                  "Congratulations, your account has been created!")
        Dim urlDigitalItemSubscription As New DigitalItem(New  _
                                  Uri("http://www.url.com/login.aspx"), _
                                  "You may now continue to login to your account.")
        Dim gRecurrentItem As New ShoppingCartItem()
        maxCharge.MaximumCharge = 29.99
        gRecurrentItem.Name = "Entry Level Plan"
        gRecurrentItem.Description = "Allows for basic stuff. Monthly Subscription:"
        gRecurrentItem.Quantity = 1
        gRecurrentItem.Price = 29.99
        gRecurrentItem.DigitalContent = urlDigitalItemSubscription
        gRecurrentItem.DigitalContent.Disposition = DisplayDisposition.Pessimistic

        urlDigitalItem.Disposition = DisplayDisposition.Pessimistic

        gSubscription.Type = SubscriptionType.google
        gSubscription.Period = GCheckout.AutoGen.DatePeriod.MONTHLY
        gSubscription.AddSubscriptionPayment(maxCharge)
        gSubscription.RecurrentItem = gRecurrentItem

        cartRequest.AddItem("Entry Level Plan", "Allows for basic stuff.", 1, gSubscription)
        cartRequest.AddItem("Entry Level Plan", "First Month:", 29.99, 1, urlDigitalItem)

        ' May use this if you want Google to track something with your order   
        cartRequest.MerchantPrivateData = "UserName:Joe87"

        Debug.WriteLine(EncodeHelper.Utf8BytesToString(cartRequest.GetXml()))

        'Dim Resp As GCheckoutResponse = Req.Send
        'If Resp.IsGood Then
        '    Response.Redirect(Resp.RedirectUrl, True)
        'Else
        '    Response.Write("Resp.ResponseXml = " + Resp.ResponseXml + "<br>")
        '    Response.Write("Resp.RedirectUrl = " + Resp.RedirectUrl + "<br>")
        '    Response.Write("Resp.IsGood = " + Resp.IsGood + "<br>")
        '    Response.Write("Resp.ErrorMessage = " + Resp.ErrorMessage + "<br>")
        'End If

    End Sub

End Module
