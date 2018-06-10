<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Index</title>
    <link href="https://rawgithub.com/hayageek/jquery-upload-file/master/css/uploadfile.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="../../Content/jquery.uploadfile.js"></script>
</head>
<body>

    <div class="row">
        <div class="col-md-4">
            <div id="fileuploader">Upload</div>
        </div>
    </div>
    <div id="extrabutton" class="ajax-file-upload-green">Start Upload</div>

    <script>
        //This example show how to use upload plugin with Asp.Net C# MVC
        //The plugin use an extraHTML, download and delete buttons
        $(document).ready(function () {
            var extraObj = $("#fileuploader").uploadFile({
                url: "../Home/UploadFile",
                statusBarWidth: 'auto',
                dragdropWidth: 'auto',
                showDelete: true,
                showDownload: true,
                autoSubmit: false,
                showProgress: true,
                extraHTML: function () {
                    var html = "<div><b>File Tags:</b><input type='text' name='tags' value='' /> <br/>";
                    html += "<b>Category</b>:<select name='category'><option value='1'>ONE</option><option value='2'>TWO</option></select>";
                    html += "</div>";
                    return html;
                },
                deleteCallback: function (data, pd) {
                    $.post("../Home/DeleteFile?url=" + data.url,
                        function (resp, textStatus, jqXHR) {
                            //Show Message	
                            console.log(resp, textStatus);
                            alert(resp.msg);
                            pd.statusbar.hide(); //You choice.
                        });
                },
                downloadCallback: function (json, pd) {
                    console.log(pd);
                    window.open('../Home/DownloadFile?url=' + json.url, "_blank");
                }
            });
            $("#extrabutton").click(function () {
                extraObj.startUpload();
            });
        });
    </script>
</body>
</html>
