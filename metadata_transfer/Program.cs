using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Granicus.MediaManager.SDK;

namespace metadata_transfer
{
    class Program
    {
        static void Main(string[] args)
        {
            string site = args[0];
            int src_clip = int.Parse(args[1]);
            int dest_clip = int.Parse(args[2]);

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string pw = Console.ReadLine();

            MediaManager mm = new MediaManager(site, username, pw);
            // transfer the event metadata to the clip

            MetaDataData[] meta_array = mm.GetClipMetaData(src_clip);

            // scrub out the UIDs and assign source_id
            foreach (MetaDataData meta in meta_array)
            {
                meta.SourceID = meta.ID;
                meta.UID = "";
            }

            // tree-ify
            mm.ConvertToMetaTree(ref meta_array);

            // upload metadata into the archive
            mm.ImportClipMetaData(dest_clip, meta_array, true, true);

            Console.WriteLine("Meta transfered from clip {0} to clip {1}.", src_clip, dest_clip);
        }
    }
}