using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameplayData
{
    [CreateAssetMenu(fileName = "ObjectDataListSO", menuName = "Scriptable/Object Data List")]
    public class ObjectDataListSO : ScriptableObject
    {
        /// <summary>
        /// Attributes of an entity on the game.
        /// </summary>
        [SerializeField]
        private List<DataAttribute> data;

        /// <summary>
        /// Get value from data list.
        /// </summary>
        /// <param name="id">ID of an attribute</param>
        /// <param name="value">Value wantted. If ther is no attribute, returns 0</param>
        /// <returns>True if the value was found</returns>
        public bool GetAttributeValue(AttributeType id, out float value)
        {
            value = 0;

            foreach (DataAttribute attribute in data)
            {
                if (attribute.id == id)
                {
                    value = attribute.value;
                    return true;
                }
            }

            Debug.LogError($"Attribute {id} not found {this.name}");
            return false;
        }

        /// <summary>
        /// Data class to storage attributes from an object.
        /// </summary>
        [Serializable]
        private class DataAttribute
        {
            public AttributeType id;
            public float value;
        }
    }

    public enum AttributeType
    {
        SCORE_VALUE  = 1,
        MAX_HEALTH   = 2,
        MAX_VELOCITY = 3,
    }
}





