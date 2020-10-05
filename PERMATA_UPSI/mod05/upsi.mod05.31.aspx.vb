Public Class upsi_mod5_31
    Inherits System.Web.UI.Page

    '    [WebMethod]
    'Public Static String FetchCustomer(String CustomerID)
    '{
    '  String response = "<p>No customer selected</p>";
    '  String connect = "Server=MyServer;Database=Northwind;Trusted_Connection=True";
    '  String query = "SELECT CompanyName, Address, City, Region, PostalCode," +
    '            "Country, Phone, Fax FROM Customers WHERE CustomerID = @CustomerID";
    '  If (CustomerID!= null && CustomerID.Length == 5)
    '  {
    '    StringBuilder sb = New StringBuilder();
    '    Using (SqlConnection conn = New SqlConnection(connect))
    '    {
    '      Using (SqlCommand cmd = New SqlCommand(query, conn))
    '      {
    '        cmd.Parameters.AddWithValue("CustomerID", CustomerID);
    '        conn.Open();
    '        SqlDataReader rdr = cmd.ExecuteReader();
    '        If (rdr.HasRows)
    '        {
    '          While (rdr.Read())
    '          {
    '            sb.Append("<p>");
    '            sb.Append("<strong>" + rdr["CompanyName"].ToString() + "</strong><br />");
    '            sb.Append(rdr["Address"].ToString() + "<br />");
    '            sb.Append(rdr["City"].ToString() + "<br />");
    '            sb.Append(rdr["Region"].ToString() + "<br />");
    '            sb.Append(rdr["PostalCode"].ToString() + "<br />");
    '            sb.Append(rdr["Country"].ToString() + "<br />");
    '            sb.Append("Phone: " + rdr["Phone"].ToString() + "<br />");
    '            sb.Append("Fax: " + rdr["Fax"].ToString() + "</p>");
    '            response = sb.ToString();
    '          }
    '        }
    '      }
    '    }
    '  }
    '  Return response;
    '}


    'Connection to database bole ikut sample code above (ni Ajax call from client side code)
    <System.Web.Services.WebMethod()>
    Public Shared Function SendClientDataToServer(ByVal name As String) As String
        Return "Hello " & name & Environment.NewLine & "The Current Time is: " &
            DateTime.Now.ToString()


    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


End Class