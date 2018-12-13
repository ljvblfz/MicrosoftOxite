//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Oxite.Modules.Setup.Extensions
{
    public static class HtmlHelperExtensions
    {
        #region WizardHeader
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="getIsChecked"></param>
        /// <param name="labelInnerHtml"></param>
        /// <returns></returns>
        /// <example>
        /// <div class="wizardHeader">
        ///     <div class="wizardStep finishedStep">Step 1</div>
        ///     <div class="wizardStepConnector finishedConnector"></div>
        ///     <div class="wizardStep currentStep">Step 2</div>
        ///     <div class="wizardStepConnector"></div>
        ///     <div class="wizardStep">Step 3</div>
        /// </div>
        /// </example>
        public static string WizardHeader(this HtmlHelper htmlHelper, List<string> steps)
        {
            StringBuilder output = new StringBuilder();

            TagBuilder wizardTag = new TagBuilder("div");

            int currentStep = 0;

            int.TryParse(htmlHelper.ViewData["CurrentWizardPage"].ToString(), out currentStep);

            wizardTag.Attributes["class"] = "wizardHeader";

            for (int i = 0; i < steps.Count; i++)
            {
                if (i > 0)
                {
                    TagBuilder connectorTag = new TagBuilder("div");

                    if (i <= currentStep)
                    {
                        connectorTag.Attributes["class"] = "wizardStepConnector finishedConnector";
                    }
                    else
                    {
                        connectorTag.Attributes["class"] = "wizardStepConnector";
                    }

                    output.Append(connectorTag.ToString());
                }

                TagBuilder builder = new TagBuilder("div");

                if (i < currentStep)
                {
                    builder.Attributes["class"] = "wizardStep finishedStep";
                }
                else if (i == currentStep)
                {
                    builder.Attributes["class"] = "wizardStep currentStep";
                }
                else
                {
                    builder.Attributes["class"] = "wizardStep";
                }

                float stepPercent = 100 / steps.Count;
                builder.Attributes["width"] = stepPercent.ToString() + "%";
                builder.InnerHtml = steps[i];

                output.Append(builder.ToString());
            }

            wizardTag.InnerHtml = output.ToString();

            return wizardTag.ToString();
        }
        #endregion
    }
}
