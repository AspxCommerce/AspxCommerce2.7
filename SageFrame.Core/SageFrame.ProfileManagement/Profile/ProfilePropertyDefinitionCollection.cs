#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using SageFrame.ProfileManagement;

#endregion


namespace SageFrame.Profile
{
    /// <summary>
    /// Profile 
    /// </summary>
    [Serializable()]
    public class ProfilePropertyDefinitionCollection : System.Collections.CollectionBase
    {


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructs a new default collection
        /// </summary>
        /// <history>
        ///    [alok]    03/25/2010    created
        /// </history>
        /// -----------------------------------------------------------------------------
        public ProfilePropertyDefinitionCollection()
        {
        }

        /// <summary>
        /// Add ranges.
        /// </summary>
        /// <param name="definitionsList">Definitions list.</param>
        public ProfilePropertyDefinitionCollection(ArrayList definitionsList)
        {
            AddRange(definitionsList);
        }

        /// <summary>
        /// Profile property definition collection.
        /// </summary>
        /// <param name="collection">ProfilePropertyDefinitionCollection object.</param>
        public ProfilePropertyDefinitionCollection(ProfilePropertyDefinitionCollection collection)
        {
            AddRange(collection);
        }

        /// <summary>
        /// Adss value to list
        /// </summary>
        /// <param name="value">ProfileManagementInfo object</param>
        /// <returns></returns>
        public int Add(ProfileManagementInfo value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PropertyTypeName"></param>
        /// <returns></returns>
        public ProfilePropertyDefinitionCollection GetByCategory(string PropertyTypeName)
        {
            ProfilePropertyDefinitionCollection collection = new ProfilePropertyDefinitionCollection();
            foreach (ProfileManagementInfo profileProperty in InnerList)
            {
                if ((profileProperty.PropertyTypeName == PropertyTypeName))
                {
                    // Found Profile property that satisfies category
                    collection.Add(profileProperty);
                }
            }
            return collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionsList"></param>
        public void AddRange(ArrayList definitionsList)
        {
            foreach (ProfileManagementInfo objProfilePropertyDefinition in definitionsList)
            {
                Add(objProfilePropertyDefinition);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(ProfilePropertyDefinitionCollection collection)
        {
            foreach (ProfileManagementInfo objProfilePropertyDefinition in collection)
            {
                Add(objProfilePropertyDefinition);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ProfileManagementInfo this[int index]
        {
            get
            {
                return ((ProfileManagementInfo)(List[index]));
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ProfileManagementInfo this[string name]
        {
            get
            {
                return GetByName(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ProfileManagementInfo GetByName(string name)
        {
            ProfileManagementInfo profileItem = null;
            foreach (ProfileManagementInfo profileProperty in InnerList)
            {
                if ((profileProperty.Name == name))
                {
                    // Found Profile property
                    profileItem = profileProperty;
                }
            }
            return profileItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(ProfileManagementInfo value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProfileManagementInfo GetById(int id)
        {
            ProfileManagementInfo profileItem = null;
            foreach (ProfileManagementInfo profileProperty in InnerList)
            {
                if ((profileProperty.ProfileID == id))
                {
                    // Found Profile property
                    profileItem = profileProperty;
                }
            }
            return profileItem;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(ProfileManagementInfo value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, ProfileManagementInfo value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Remove(ProfileManagementInfo value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Sort()
        {
            InnerList.Sort(new ProfilePropertyDefinitionComparer());
        }


    }
}
