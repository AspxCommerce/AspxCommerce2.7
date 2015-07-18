Imports System
Imports System.Net
imports System.Security.Cryptography.X509Certificates

Public Class TrustAllCertificatePolicy
    Implements System.Net.ICertificatePolicy

    Public Sub New()
        MyBase.New()

    End Sub

    Public Function CheckValidationResult(ByVal sp As ServicePoint, ByVal cert As X509Certificate, ByVal req As WebRequest, _
        ByVal problem As Integer) As Boolean Implements ICertificatePolicy.CheckValidationResult
        Return True
    End Function
End Class


