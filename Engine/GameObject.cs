using Engine.Primitives;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Engine
{
    [XmlInclude(typeof(Cube))]
	public class GameObject : GameEntity
	{
		// NOTE(françois): transform itself can not be modified, only its attributes.
        public Transform transform { get; set; } = new Transform();
		
		// TODO?/FIXME?: Btw, should this be moved to GameEntity?
	    public string Name { get; set; }
	    
	   	// FIXME?: The fact that this constructor needs a name means that every derived class of GameObject
		//  needs to implement it so they can be instantiated in Scene.Instantiate<T>.
		//  Otherwhise, there needs to be two different constructors, one which takes a name and another that does not.
		//  (A default value for the parameter did not change anything)
        public GameObject(string name) {
            this.Name = name;
        }

        public void Save(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(GameObject));
                XML.Serialize(stream, this);
            }
        }

        public static GameObject LoadFromFile(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var XML = new XmlSerializer(typeof(GameObject));

                return (GameObject)XML.Deserialize(stream);
            }
        }

        public GameObject() { }
    }

}
