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
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace GCheckout.NotificationQueue
    Public Class NotificationFile

        Private _Name As String = Nothing

        Private _CreationTime As DateTime

        Private _Id As String

        Private _Type As String

        Private _OrderId As String

        Public Sub New(ByVal Name As String, ByVal CreationTime As DateTime)
            MyBase.New()
            _Name = Name
            _CreationTime = CreationTime
            Dim Re As Regex = New Regex("(.+)--(.+)--(.+).xml")
            Dim M As Match = Re.Match(Name)
            If M.Success Then
                _Type = M.Groups(1).ToString
                _OrderId = M.Groups(2).ToString
                _Id = M.Groups(3).ToString
            Else
                Throw New ApplicationException(String.Format(("Notification file name '{0}' is not on the recognized " + "'x--y--z.xml' format."), Name))
            End If
        End Sub

        Public Sub New(ByVal Id As String, ByVal Type As String, ByVal OrderId As String)
            MyBase.New()
            _Id = Id
            _Type = Type
            _OrderId = OrderId
            _Name = String.Format("{0}--{1}--{2}.xml", Type, OrderId, Id)
        End Sub

        Public ReadOnly Property Age() As TimeSpan
            Get
                Return DateTime.Now.Subtract(_CreationTime)
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

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
    End Class
End Namespace
