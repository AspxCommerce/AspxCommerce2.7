//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, 
// either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): RetroViz Design. 
// Visit: http://www.retroviz.com
//------------------------------------------------------------------------------

var FCKNopCombo_command = function(name) {
    this.Name = name;
}

FCKNopCombo_command.prototype.Execute = function(itemText, itemLabel) {
    if (itemText != "")
        FCK.InsertHtml(itemText);
}

FCKNopCombo_command.prototype.GetState = function() {
    return;
}

FCKCommands.RegisterCommand('mynopcombocommand', new FCKNopCombo_command('any_name'));

var FCKToolbarNopCombo = function(tooltip, style) {
    this.Command = FCKCommands.GetCommand('mynopcombocommand');
    this.CommandName = 'mynopcombocommand';
    this.Label = this.GetLabel();
    this.Tooltip = tooltip ? tooltip : this.Label;
    this.Style = style;
    this.FieldWidth = 200;
    this.PanelWidth = 200;
};

FCKToolbarNopCombo.prototype = new FCKToolbarSpecialCombo;

//Label to appear in the FCK toolbar
FCKToolbarNopCombo.prototype.GetLabel = function() {
    return "Message&nbsp;Tokens";
};

//Retrieve tokens from xml and add to combo
FCKToolbarNopCombo.prototype.CreateItems = function() {
    var tokensXmlPath = FCKConfig.NopTokensXmlPath;

    // Load the XML file into a FCKXml object.
    var xml = new FCKXml();
    xml.LoadUrl(tokensXmlPath);

    var tokensXmlObj = FCKXml.TransformToObject(xml.SelectSingleNode('Tokens'));

    // Get the "Token" nodes defined in the XML file.
    var tokenNodes = tokensXmlObj.$Token;

    // Add each style to our "Tokens" collection.
    for (var i = 0; i < tokenNodes.length; i++) {
        var tokenNode = tokenNodes[i];

        var tokenValue = (tokenNode.value || '').toLowerCase();

        if (tokenValue.length == 0)
            throw ('The element name is required. Error loading "' + tokensXmlPath + '"');

        // Set styles and labels to the dropdown
        this._Combo.AddItem(tokenNode.value, '<span style="color:#000000;font-weight: normal; font-size: 12px;">' + tokenNode.name + '</span>');
    }
}

//Register the combo with the FCKEditor
FCKToolbarItems.RegisterItem('nopcombo', new FCKToolbarNopCombo('Nop Combo', FCK_TOOLBARITEM_ICONTEXT)); //or FCK_TOOLBARITEM_ONLYTEXT


