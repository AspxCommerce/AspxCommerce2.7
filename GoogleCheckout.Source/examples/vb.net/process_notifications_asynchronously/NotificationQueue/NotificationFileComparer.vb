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

    Public Class NotificationFileComparer
        Implements IComparer

        Public Sub New()
            MyBase.New()

        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim FileX As NotificationFile = CType(x, NotificationFile)
            Dim FileY As NotificationFile = CType(y, NotificationFile)
            Return FileY.Age.CompareTo(FileX.Age)
        End Function
    End Class
End Namespace