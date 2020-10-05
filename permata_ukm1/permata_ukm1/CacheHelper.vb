Public NotInheritable Class CacheHelper

    Private Sub New()
    End Sub

    Public Shared Sub CacheThis(ByVal response As HttpResponse, ByVal intSeconds As Integer)

        ' Only cache for X seconds.
        response.Cache.SetExpires(DateTime.Now.AddSeconds(intSeconds))
        response.Cache.SetMaxAge(New TimeSpan(0, 0, intSeconds))
        response.Cache.SetCacheability(HttpCacheability.[Public])
        response.Cache.SetValidUntilExpires(True)

        ' Sliding expiration means we reset the X seconds after each request.
        ' SetETag is required to work around one aspect of sliding expiration.
        response.Cache.SetSlidingExpiration(True)
        response.Cache.SetETagFromFileDependencies()
    End Sub

End Class
