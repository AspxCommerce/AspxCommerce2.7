#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion


namespace SageFrame.NewLetterSubscriber
{
    /// <summary>
    /// Entities class for NewsLetterSettingsInfo
    /// </summary>
    public class NewsLetterSettingsInfo
    {
        #region Constructor
        /// <summary>
        /// Initializes an instance of NewsLetterSettingsInfo.
        /// </summary>
        public NewsLetterSettingsInfo()
        {
        }
        #endregion

        #region "Private Members"
        private string _submitButtonText = string.Empty;
        private string _subscriptionHelpText = string.Empty;
        private string _subscriptionModuleTitle= string.Empty;
        private string _subscriptionType = string.Empty;
        private string _textBoxWaterMarkText = string.Empty;
        #endregion

        #region "Public Properties"
        /// <summary>
        /// Gets or sets submit button text.
        /// </summary>
        public string SubmitButtonText
        {
            get { return _submitButtonText; }
            set { _submitButtonText = value; }
        }
        /// <summary>
        /// Gets or sets subscription help text.
        /// </summary>
        public string SubscriptionHelpText
        {
            get { return _subscriptionHelpText; }
            set { _subscriptionHelpText = value; }
        }

        /// <summary>
        /// Gets or sets subscription module title.
        /// </summary>
        public string SubscriptionModuleTitle
        {
            get { return _subscriptionModuleTitle; }
            set { _subscriptionModuleTitle = value; }
        }

        /// <summary>
        /// Gets or sets subscription type.
        /// </summary>
        public string SubscriptionType
        {
            get { return _subscriptionType; }
            set { _subscriptionType = value; }
        }

        /// <summary>
        /// Gets or sets text box water mark text.
        /// </summary>
        public string TextBoxWaterMarkText
        {
            get { return _textBoxWaterMarkText; }
            set { _textBoxWaterMarkText = value; }
        }
        #endregion        
    }
}
