using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace CustomContentLoader
{
    public class AuthenticatingContentLoader : INavigationContentLoader
    {        
        public string LoginPage
        {
            get;
            set;
        }

        public string SecuredFolder
        {
            get;
            set;
        }
               
        private PageResourceContentLoader loader = new PageResourceContentLoader();

        public IAsyncResult BeginLoad(Uri targetUri, Uri currentUri, AsyncCallback userCallback, object asyncState)
        {
            if (!App.UserIsAuthenticated)
            {
                if ((System.IO.Path.GetDirectoryName(targetUri.ToString()).Trim('\\') == SecuredFolder) &&
                    (targetUri.ToString() != LoginPage))
                {
                    targetUri = new Uri(LoginPage, UriKind.Relative);
                }
            }
            return loader.BeginLoad(targetUri, currentUri, userCallback, asyncState);
        }

        public bool CanLoad(Uri targetUri, Uri currentUri)
        {
            return loader.CanLoad(targetUri, currentUri);
        }

        public void CancelLoad(IAsyncResult asyncResult)
        {
            loader.CancelLoad(asyncResult);
        }

        public LoadResult EndLoad(IAsyncResult asyncResult)
        {
            return loader.EndLoad(asyncResult);            
        }
    }
}
