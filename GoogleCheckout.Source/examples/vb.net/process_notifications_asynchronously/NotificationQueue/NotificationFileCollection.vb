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
Imports System.Collections

Namespace GCheckout.NotificationQueue

    Public Class NotificationFileCollection

        Private _NFiles As ArrayList

        Public Sub New()
            MyBase.New()
            _NFiles = New ArrayList
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                Return _NFiles.Count
            End Get
        End Property

        Public Sub Add(ByVal NFile As NotificationFile)
            _NFiles.Add(NFile)
        End Sub

        Public Function GetFileAt(ByVal Index As Integer) As NotificationFile
            Return CType(_NFiles(Index), NotificationFile)
        End Function

        Public Sub RemoveFileAt(ByVal Index As Integer)
            _NFiles.RemoveAt(Index)
        End Sub

        Public Sub RemoveNewFiles(ByVal MinAgeSeconds As Integer)
            Dim i As Integer = (_NFiles.Count - 1)
            Do While (i >= 0)
                Dim NFile As NotificationFile = CType(_NFiles(i), NotificationFile)
                If (NFile.Age.TotalSeconds < MinAgeSeconds) Then
                    _NFiles.RemoveAt(i)
                End If
                i = (i - 1)
            Loop
        End Sub

        Public Sub SortByAge()
            _NFiles.Sort(New NotificationFileComparer)
        End Sub
    End Class
End Namespace

