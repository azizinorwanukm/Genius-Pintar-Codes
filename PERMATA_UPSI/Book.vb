
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web


Public Class Book
        Public Property BooktitleId() As Long
            Get
                Return m_BooktitleId
            End Get
            Set
                m_BooktitleId = Value
            End Set
        End Property
        Private m_BooktitleId As Long
        Public Property BookTitle() As String
            Get
                Return m_BookTitle
            End Get
            Set
                m_BookTitle = Value
            End Set
        End Property
        Private m_BookTitle As String
    End Class
