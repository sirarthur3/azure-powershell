﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.FrontDoor.Common;
using Microsoft.Azure.Commands.FrontDoor.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;

namespace Microsoft.Azure.Commands.FrontDoor.Cmdlets
{
    /// <summary>
    /// Defines the New-AzFrontDoorWafManagedRuleExclusionObject cmdlet.
    /// </summary>
    [Cmdlet("New", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "FrontDoorWafManagedRuleExclusionObject"), OutputType(typeof(PSManagedRuleExclusion))]
    public class NewAzureRmFrontDoorWafManagedRuleExclusionObject : AzureFrontDoorCmdletBase
    {
        /// <summary>
        /// Exclusion match variable
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "Match variable")]
        [PSArgumentCompleter("RequestHeaderNames", "RequestCookieNames", "QueryStringArgNames", "RequestBodyPostArgNames")]
        public string Variable { get; set; }

        /// <summary>
        /// Exclusion operator
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "Operator")]
        [PSArgumentCompleter("Equals", "Contains", "StartsWith", "EndsWith", "EqualsAny")]
        public string Operator { get; set; }

        /// <summary>
        /// Exclusion selector
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Selector")]
        public string Selector { get; set; }

        public override void ExecuteCmdlet()
        {
            ValidateArguments();

            var rule = new PSManagedRuleExclusion
            {
                MatchVariable = Variable,
                SelectorMatchOperator = Operator,
                Selector = Selector
            };
            WriteObject(rule);
        }

        private void ValidateArguments()
        {
            if (Operator == PSExclusionOperatorProperty.EqualsAny.ToString() && Selector != null)
            {
                throw new PSArgumentException(nameof(Selector));
            }

            if (Operator != PSExclusionOperatorProperty.EqualsAny.ToString() && (Selector == null || Selector.Length == 0))
            {
                throw new PSArgumentNullException(nameof(Selector));
            }
        }
    }
}
