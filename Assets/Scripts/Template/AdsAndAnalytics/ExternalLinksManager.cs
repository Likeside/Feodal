using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Template.AdsAndAnalytics {
    public class ExternalLinksManager: GlobalSingleton<ExternalLinksManager> {

        [SerializeField] ExternalLinksConfigSO _config;
        
        public event Action OnRateUsLinkOpened;

        public void OpenTermsLink() {
            Debug.Log("Opening terms link");
            StartCoroutine(OpenLinkCoroutine(_config.termsLink));
        }

        public void OpenPolicyLink() {
            Debug.Log("Opening policy link");
            Application.OpenURL(_config.policyLink);
        }

        public void OpenRateUsLink() {
            Debug.Log("Opening rate us link");
            StartCoroutine(OpenLinkCoroutine(_config.rateUsLink));
            OnRateUsLinkOpened?.Invoke();
        }

        IEnumerator OpenLinkCoroutine(string url) {
            yield return new WaitForSeconds(0.1f);
            Application.OpenURL(url);
        }
    }
}