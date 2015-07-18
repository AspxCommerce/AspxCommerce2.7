'/*************************************************
' * Copyright (C) 2007 Google Inc.
' *
' * Licensed under the Apache License, Version 2.0 (the "License");
' * you may not use this file except in compliance with the License.
' * You may obtain a copy of the License at
' *
' *      http://www.apache.org/licenses/LICENSE-2.0
' *
' * Unless required by applicable law or agreed to in writing, software
' * distributed under the License is distributed on an "AS IS" BASIS,
' * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' * See the License for the specific language governing permissions and
' * limitations under the License.
'*************************************************/

Imports System

Namespace GCheckout.NotificationQueue

    Public Class NotificationQueueMessage
        Private _Id As String
        Private _Type As String
        Private _OrderId As String
        Private _Xml As String

        Public Sub New(ByVal Id As String, ByVal Type As String, ByVal OrderId As String, ByVal Xml As String)
            _Id = Id
            _Type = Type
            _OrderId = OrderId
            _Xml = Xml
        End Sub

        Public ReadOnly Property Id() As String
            Get
                Return _Id
            End Get
        End Property

        Public ReadOnly Property Type() As String
            Get
                Return _Type
            End Get
        End Property

        Public ReadOnly Property OrderId() As String
            Get
                Return _OrderId
            End Get
        End Property

        Public ReadOnly Property Xml() As String
            Get
                Return _Xml
            End Get
        End Property
    End Class
End Namespace