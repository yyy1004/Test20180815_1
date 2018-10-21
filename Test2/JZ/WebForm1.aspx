
2
3
4
5
6
7
8
9
10
11
12
13
14
15
16
17
18
19
20
21
22
23
24
25
26
27
28
29
30
31
32
33
34
35
36
37
38
39
40
41
42
43
44
45
46
 
<html>
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>无刷新上传文件 - 页面只有一个上传</title>
    <!--<link rel="stylesheet" href="css/WizardWuUpload_Blue.css" />-->
    <link rel="stylesheet" href="css/WizardWuUpload_Green.css" />
    <!--<link rel="stylesheet" href="css/WizardWuUpload_Silver.css" />-->
 
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="js/jquery.form.js"></script>
    <script type="text/javascript" src="js/WizardWuUpload.js"></script>
    <script type="text/javascript">
      $(function () {
        //傳入的參數，為該 divUploadArea 區塊中，所有 DOM 对象(控件) 的 id 編號，如: divUploadArea1、spanUploadSelect1、WizardWuFileUpload1、...
        $(this).loadUploadContent('1'); //載入第一個上傳區塊 divUploadArea1 所需要的 form 及 jQuery 內容
      });
    </script>
</head>
 
<body>
  <h2>WizardWu 无刷新上传文件 - 页面只有一个上传</h2>
  <table>
    <tr>
      <td>
        <p>只允许上传 jpg/jpeg/gif/png/bmp 格式的图片，图片大小不能超过 1 MB</p>
 
        <div class="UploadArea" id="divUploadArea1">
          <div class="UploadButton">
            <span id="spanUploadSelect1">选择文件</span>
            <!--上传用的file控件-->
            <input id="WizardWuFileUpload1" type="file" name="此处name可随便命名，name名称可重复" />
          </div>
          <!--显示加载进度-->
          <div class="UploadProgress" id="divUploadProgress1">
            <span class="UploadBar" id="spanUploadBar1"></span><span class="UploadPercent" id="spanUploadPercent1">0%</span>
          </div>
          <!--显示已经上传的文件名、上传成功才会出现的「删除图片」字样超连接、删除成功才会出现的「删除成功」字样-->
          <div class="ShowContentAfterUploadSuccess" id="divShowContentAfterUploadSuccess1"></div>
          <!--显示已经上传的图片-->
          <div id="divShowImageAfterUploadSuccess1"></div>
        </div>
      </td>
    </tr>
  </table>
</body>
</html>
 