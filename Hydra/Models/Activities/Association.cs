using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Activities {

    [DataContract]
    public class Association {

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "internalName")]
        public string InternalName { get; set; }

        [DataMember(Name = "parentAssociation")]
        public string ParentAssociation { get; set; }

        public override string ToString() {
            return this.DisplayName;
        }
    }
}
