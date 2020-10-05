Imports System.Web.UI.WebControls

Public Class MenuTreeview
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LoadTreeview()
        Catch ex As Exception
            Message.Text = "Error message:" & ex.Message
        End Try
    End Sub

    Private Sub LoadTreeview()
        ' Create a new TreeView control.
        Dim NewTree As New TreeView

        Try
            ' Set the properties of the TreeView control.
            NewTree.ID = "MenuTreeView"
            NewTree.DataSourceID = "MenuXmlDataSource"
            NewTree.CssClass = "TreeView"
            NewTree.RootNodeStyle.CssClass = "RootNodeStyle"
            NewTree.ParentNodeStyle.CssClass = "ParentNodeStyle"
            NewTree.SelectedNodeStyle.CssClass = "SelectedNodeStyle"
            NewTree.LeafNodeStyle.CssClass = "LeafNodesStyle"
            NewTree.HoverNodeStyle.CssClass = "HoverNodeStyle"
            'NewTree.NodeStyle.CssClass = "NodeStyle"

            ' Create the tree node binding relationship.

            ' Create the root node binding.
            Dim RootBinding As New TreeNodeBinding
            RootBinding.DataMember = "Root"
            RootBinding.TextField = "Title"

            ' Create the parent node binding.
            Dim ParentBinding As New TreeNodeBinding
            ParentBinding.DataMember = "Chapter"
            ParentBinding.TextField = "Heading"

            ' Create the leaf node binding.
            Dim LeafBinding As New TreeNodeBinding
            LeafBinding.DataMember = "Section"
            LeafBinding.TextField = "Heading"
            LeafBinding.NavigateUrlField = "NavigateUrl"
            LeafBinding.ImageUrlField = "ImageUrl"

            ' Add bindings to the DataBindings collection.
            NewTree.DataBindings.Add(RootBinding)
            NewTree.DataBindings.Add(ParentBinding)
            NewTree.DataBindings.Add(LeafBinding)

            ' Manually register the event handler for the SelectedNodeChanged event.
            AddHandler NewTree.SelectedNodeChanged, AddressOf Node_Change

            ' Add the TreeView control to the Controls collection of the PlaceHolder control.
            ControlPlaceHolder.Controls.Add(NewTree)

        Catch ex As Exception

        End Try

    End Sub

    Sub Node_Change(ByVal sender As Object, ByVal e As EventArgs)

        ' Retrieve the TreeView control from the Controls collection of the PlaceHolder control.
        Dim LocalTree As TreeView = CType(ControlPlaceHolder.FindControl("MenuTreeView"), TreeView)

        ' Display the selected node.
        Message.Text = "You selected: " & LocalTree.SelectedNode.Text & " URL:" & LocalTree.SelectedNode.Value

        '--TODO here

    End Sub


End Class