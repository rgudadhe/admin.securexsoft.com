<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 1</title>
<style type="text/css">

.feedbackform{
padding: 5px;
}

div.fieldwrapper{ /*field row DIV (includes two columns- Styled label column and 'thefield' column)*/
width: 550px; /*width of form rows*/
overflow: hidden;
padding: 5px 0;
}

div.fieldwrapper label.styled{ /* label elements that should be styled (left column within fieldwrapper DIV) */
float: left;
width: 150px; /*width of label (left column)*/
text-transform: uppercase;
font-weight: bold;
border-bottom: 1px solid red;
margin-right: 15px; /*spacing with right column*/
}

div.fieldwrapper div.thefield{ /* DIV that wraps around the actual form fields (right column within fieldwrapper DIV) */
float: left;
margin-bottom: 10px; /* space following the field */
}

div.fieldwrapper div.thefield input[type="text"]{ /* style for INPUT type="text" fields. Has no effect in IE7 or below! */
width: 250px;
}

div.fieldwrapper div.thefield textarea{ /* style for TEXTAREA fields. */
width: 300px;
height: 150px;
}

div.buttonsdiv{ /*div that wraps around the submit/reset buttons*/
margin-top: 5px; /*space above buttonsdiv*/
}

div.buttonsdiv input{ /* style for INPUT fields within 'buttonsdiv'. Assumed to be form buttons. */
width: 80px;
background: #e1dfe0;
}


@media screen and (max-width: 480px){ /* responsive form settings, when window width is 480px or less */

	div.fieldwrapper{
		width: auto;
		overflow: auto;
	}

	div.fieldwrapper label.styled{
		float: none;
		width: auto; /*width of label (left column)*/
		text-transform: uppercase;
		border-bottom: 1px solid red;
		margin-right: auto; /*spacing with right column*/
	}

	div.fieldwrapper div.thefield{
		float: none;
		margin-bottom: 10px; /* space following the field */
	}

	div.fieldwrapper div.thefield *:first-of-type{
		margin-top: 10px;
	}

	div.fieldwrapper div.thefield input[type="text"]{
		width: 95%;
	}
	
	div.fieldwrapper div.thefield textarea{
		width: 95%;
	}

}

</style>




</head>

<body>
<form class="feedbackform">

<div class="fieldwrapper">
<label for="username" class="styled">Your Name:</label>
<div class="thefield">
<input type="text" id="username" value="" size="30" />
</div>
</div>

<div class="fieldwrapper">
<label for="email" class="styled">Email address:</label>
<div class="thefield">
<input type="text" id="email" value="" size="30" /><br />
<span style="font-size: 80%">*Note: Please make sure it's correctly 
entered!</span>
</div>
</div>

<div class="fieldwrapper">
<label for="somehighschool" class="styled">education:</label>
<div class="thefield">
<ul style="margin-top:0;">
<li><input type="radio" id="somehighschool" name="education" value=""/> <label 
for="somehighschool">Some Highschool</label></li>
<li><input type="radio" id="highschool" name="education" value="" /> <label 
for="highschool">Highschool graduate</label></li>
<li><input type="radio" id="somecollege" name="education" value="" /> <label 
for="somecollege">Some college</label></li>
<li><input type="radio" id="vocation" name="education" value="" /> <label 
for="vocation">Vocation school</label></li>
<li><input type="radio" id="college" name="education" value="" /> <label 
for="college">College graduate or higher</label></li>
</ul>
</div>
</div>

<div class="fieldwrapper">
<label for="html" class="styled">Skills:</label>
<div class="thefield">
<ul style="margin-top:0;">
<li><input type="checkbox" id="html" name="skills" value="" /> <label 
for="html">HTML/ CSS</label></li>
<li><input type="checkbox" id="javascript" name="skills" value=""/> <label for="javascript">JavaScript</label></li>
<li><input type="checkbox" id="ajax" name="skills" value="" /> <label for="ajax">Ajax 
and XML</label></li>
<li><input type="checkbox" id="php" name="skills" value="" /> <label for="php">PHP 
and Database</label></li>
</ul>
<span style="font-size: 80%">* Please check all that apply.</span>
</div>
</div>

<div class="fieldwrapper">
<label for="agegroup" class="styled">Department:</label>
<div class="thefield">
<select id="agegroup">
<option value="2.1">HR department</option>
<option value="3">Sales</option>
<option value="4.1">Customer Service/ Support</option>
<option value="5.2">Accounting</option>
</select>
</div>
</div>

<div class="fieldwrapper">
<label for="about" class="styled">About yourself:</label>
<div class="thefield">
<textarea id="about"></textarea>
</div>
</div>

<div class="buttonsdiv">
<input type="submit" value="Submit" style="margin-left: 150px;" /> <input 
type="reset" value="Reset" />
</div>

</form>

</body>

</html>