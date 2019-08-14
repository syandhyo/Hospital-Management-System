<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuthenticateUser.aspx.cs" Inherits="AuthenticateUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Authenticate user</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            showChooseBranch();

            $("#selBranch").change(function () {
                var branchid = this.value;
                if(branchid!=0)
                get_branchspecificdata(branchid);
            });


        });
        function showChooseBranch() {
            
            getbranch_multi();
        }
        function getbranch_multi() {
            $.ajax({
                url: "AuthenticateUser.aspx/getData",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //data: JSON.stringify(qObj),
                success: function (result) {
                    
                    if (result != null && result.d != null) {
                        var data = result.d;
                        data = $.parseJSON(data);
                        if (data.isAuthenticated == true) {
                            if (data.howmanyrows == 0) {
                                //redirect to home as session is already set
                                window.location.href = "home.aspx";
                            }
                            else {
                                //populate and show modals
                                var $dropdown = $("#selBranch");
                                $dropdown.append($("<option />").val(0).text('Select Branch'));
                                $.each(data.branchdata.Table, function () {
                                    
                                    $dropdown.append($("<option />").val(this.BRANCH_ID).text(this.BRANCH_NAME));
                                });
                                $('#myModal').modal('show');
                            }
                        }
                        else {
                            alert("You are not Authorized... Please contact Administrator.");
                        }
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
    }

        function get_branchspecificdata(branchid) {
            $.ajax({
                url: "AuthenticateUser.aspx/getbranchData",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ branchid: branchid }),
                success: function (result) {
                    
                    if (result != null && result.d != null) {
                        var data = result.d;
                        data = $.parseJSON(data);
                        if (data.isAuthenticated == true) {
                            window.location.href = "home.aspx";
                        }
                        else {
                            alert("You are not Authorized... Please contact Administrator.");
                        }
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- Modal -->
  <div class="modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
          <h4 class="modal-title">Choose Branch</h4>
        </div>
        <div class="modal-body">
           <select class="form-control mb-3 custom-select" id="selBranch" name="branchlist">
</select>
        </div>
        <div class="modal-footer">
          <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
        </div>
      </div>
      
    </div>
  </div>
    
    </form>
</body>
</html>
