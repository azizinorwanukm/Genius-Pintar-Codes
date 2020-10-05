<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="jvm.help.aspx.vb" Inherits="PERMATA_EQTest.jvm_help" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <b>JAVA VIRTUAL MACHINE INSTRUCTION</b>
            <hr />
            <applet name="mymaze" code="Maze.class" codebase="MazeJava" width="400" height="300"
                alt="A Java Applet.  If you can read this, your browser doesn't support Java"
                id="Applet1">
                                <param name="tempurl" value="" />
                                <param name="bgc_red" value="255" />
                                <param name="bgc_green" value="255" />
                                <param name="num_rows" value="4" />
                                <param name="num_columns" value="4" />
                                <param name="bgc_blue" value="255" />
                            </applet>
            <br />
            <br />
            Can you see the MAZE above?
            <hr />
            In order to answer questions 177 until 183, your PC needs JVM to be installed. What should I do?.
            <ul>
                <li><a href="https://java.com/en/download/index.jsp" target="_blank">Download JVM!</a> Once you had installed it, you should be able to see the sample java applet from Java website.</li>
                <li><a href="https://www.java.com/en/download/help/jcp_security.xml" target="_blank">Update Java Security setting inside Java Control Panel.</a><br />
                </li>
                <li>
                    <img src="img/java_panel.png" alt="java panel" />
                </li>
                <li>You need to add "http://eqtest.permatapintar.edu.my/" into Exception Site List. Once DONE, you should be able to see the MAZE. Click on it to draw the line.
                </li>

            </ul>
            <img src="img/maze.png" alt="maze" width="600" height="250" />
            <br />
            Sample MAZE image.
        </div>
    </form>
</body>
</html>
