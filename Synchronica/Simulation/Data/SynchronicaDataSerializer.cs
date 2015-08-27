﻿using FlatBuffers;
using FlatBuffers.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchronica.Simulation.Data
{
    public sealed class SynchronicaDataSerializer
    {
        #region Serialization

        public byte[] Serialize(int id, SynchronicaData data)
        {
            if (data == null)
                return null;

            var fbb = new FlatBufferBuilder(1024);

            var vScenes = SerializeScenes(fbb, data.Scenes.ToArray());
            var oData = Schema.SynchronicaData.CreateSynchronicaData(fbb, vScenes);
            Schema.SynchronicaData.FinishSynchronicaDataBuffer(fbb, oData);

            return FlatBufferExtensions.ToProtocolMessage(fbb, id);
        }

        private VectorOffset SerializeScenes(FlatBufferBuilder fbb, SceneData[] scenes)
        {
            var oScenes = Array.ConvertAll(scenes, scene => SerializeScene(fbb, scene));

            return Schema.SynchronicaData.CreateScenesVector(fbb, oScenes);
        }

        private Offset<Schema.SceneData> SerializeScene(FlatBufferBuilder fbb, SceneData scene)
        {
            var vObjects = SerializeGameObjects(fbb, scene.Objects.ToArray());

            return Schema.SceneData.CreateSceneData(fbb, scene.StartMilliseconds, scene.EndMilliseconds, vObjects);
        }

        private VectorOffset SerializeGameObjects(FlatBufferBuilder fbb, GameObjectData[] gameObjects)
        {
            var oObjects = Array.ConvertAll(gameObjects, gameObject => SerializeGameObject(fbb, gameObject));

            return Schema.SceneData.CreateObjectsVector(fbb, oObjects);
        }

        private Offset<Schema.GameObjectData> SerializeGameObject(FlatBufferBuilder fbb, GameObjectData gameObject)
        {
            var vProperties = SerializeProperties(fbb, gameObject.Variables.ToArray());

            return Schema.GameObjectData.CreateGameObjectData(fbb, gameObject.Id, vProperties);
        }

        private VectorOffset SerializeProperties(FlatBufferBuilder fbb, VariableData[] properties)
        {
            var oProperties = Array.ConvertAll(properties, variable => SerializeVariable(fbb, variable));

            return Schema.GameObjectData.CreatePropertiesVector(fbb, oProperties);
        }

        private Offset<Schema.VariableData> SerializeVariable(FlatBufferBuilder fbb, VariableData variable)
        {
            var vFrames = SerializeFrames(fbb, variable.Frames.ToArray());

            return Schema.VariableData.CreateVariableData(fbb, variable.Id, vFrames);
        }

        private VectorOffset SerializeFrames(FlatBufferBuilder fbb, KeyFrameData[] keyFrames)
        {
            var oFrames = Array.ConvertAll(keyFrames, frame => SerializeFrame(fbb, frame));

            return Schema.VariableData.CreateKeyFramesVector(fbb, oFrames);
        }

        private Offset<Schema.KeyFrameData> SerializeFrame(FlatBufferBuilder fbb, KeyFrameData frame)
        {
            if (frame is LinearKeyFrameData)
                return SerializeLinearFrame(fbb, (LinearKeyFrameData)frame);

            if (frame is PulseKeyFrameData)
                return SerializePulseFrame(fbb, (PulseKeyFrameData)frame);

            if (frame is StepKeyFrameData)
                return SerializeStepFrame(fbb, (StepKeyFrameData)frame);

            throw new NotSupportedException("Unsupported type of KeyFrameData");
        }

        private Offset<Schema.KeyFrameData> SerializeLinearFrame(FlatBufferBuilder fbb, LinearKeyFrameData frame)
        {
            if (frame.Value is short)
            {
                var oFrame = Schema.LinearKeyFrameData_Int16.CreateLinearKeyFrameData_Int16(fbb, frame.Milliseconds, (short)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.LinearKeyFrameData_Int16, oFrame.Value);
            }

            if (frame.Value is int)
            {
                var oFrame = Schema.LinearKeyFrameData_Int32.CreateLinearKeyFrameData_Int32(fbb, frame.Milliseconds, (int)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.LinearKeyFrameData_Int32, oFrame.Value);
            }

            if (frame.Value is long)
            {
                var oFrame = Schema.LinearKeyFrameData_Int64.CreateLinearKeyFrameData_Int64(fbb, frame.Milliseconds, (long)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.LinearKeyFrameData_Int64, oFrame.Value);
            }

            if (frame.Value is float)
            {
                var oFrame = Schema.LinearKeyFrameData_Float.CreateLinearKeyFrameData_Float(fbb, frame.Milliseconds, (short)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.LinearKeyFrameData_Float, oFrame.Value);
            }

            throw new NotSupportedException("Unsupported type of Value");
        }

        private Offset<Schema.KeyFrameData> SerializePulseFrame(FlatBufferBuilder fbb, PulseKeyFrameData frame)
        {
            if (frame.Value is short)
            {
                var oFrame = Schema.PulseKeyFrameData_Int16.CreatePulseKeyFrameData_Int16(fbb, frame.Milliseconds, (short)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.PulseKeyFrameData_Int16, oFrame.Value);
            }

            if (frame.Value is int)
            {
                var oFrame = Schema.PulseKeyFrameData_Int32.CreatePulseKeyFrameData_Int32(fbb, frame.Milliseconds, (int)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.PulseKeyFrameData_Int32, oFrame.Value);
            }

            if (frame.Value is long)
            {
                var oFrame = Schema.PulseKeyFrameData_Int64.CreatePulseKeyFrameData_Int64(fbb, frame.Milliseconds, (long)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.PulseKeyFrameData_Int64, oFrame.Value);
            }

            if (frame.Value is float)
            {
                var oFrame = Schema.PulseKeyFrameData_Float.CreatePulseKeyFrameData_Float(fbb, frame.Milliseconds, (short)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.PulseKeyFrameData_Float, oFrame.Value);
            }

            throw new NotSupportedException("Unsupported type of Value");
        }

        private Offset<Schema.KeyFrameData> SerializeStepFrame(FlatBufferBuilder fbb, StepKeyFrameData frame)
        {
            if (frame.Value is short)
            {
                var oFrame = Schema.StepKeyFrameData_Int16.CreateStepKeyFrameData_Int16(fbb, frame.Milliseconds, (short)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.StepKeyFrameData_Int16, oFrame.Value);
            }

            if (frame.Value is int)
            {
                var oFrame = Schema.StepKeyFrameData_Int32.CreateStepKeyFrameData_Int32(fbb, frame.Milliseconds, (int)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.StepKeyFrameData_Int32, oFrame.Value);
            }

            if (frame.Value is long)
            {
                var oFrame = Schema.StepKeyFrameData_Int64.CreateStepKeyFrameData_Int64(fbb, frame.Milliseconds, (long)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.StepKeyFrameData_Int64, oFrame.Value);
            }

            if (frame.Value is float)
            {
                var oFrame = Schema.StepKeyFrameData_Float.CreateStepKeyFrameData_Float(fbb, frame.Milliseconds, (short)frame.Value);

                return Schema.KeyFrameData.CreateKeyFrameData(fbb, Schema.KeyFrameUnion.StepKeyFrameData_Float, oFrame.Value);
            }

            throw new NotSupportedException("Unsupported type of Value");
        }

        #endregion

        #region Deserialization

        public SynchronicaData Deserialize(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;

            var buffer = new ByteBuffer(data);
            var fSynchronicaData = Schema.SynchronicaData.GetRootAsSynchronicaData(buffer);

            return Deserialize(fSynchronicaData);
        }

        public SynchronicaData Deserialize(Schema.SynchronicaData data)
        {
            var synchronicaData = new SynchronicaData();

            foreach (var scene in DeserializeScenes(data))
                synchronicaData.AddScene(scene);

            return synchronicaData;
        }

        private IEnumerable<SceneData> DeserializeScenes(Schema.SynchronicaData data)
        {
            for (int i = 0; i < data.ScenesLength; i++)
            {
                var fScene = data.GetScenes(i);

                var scene = new SceneData(fScene.StartMilliseconds, fScene.EndMilliseconds);
                foreach (var obj in DeserializeObjects(fScene))
                    scene.AddObject(obj);

                yield return scene;
            }
        }

        private IEnumerable<GameObjectData> DeserializeObjects(Schema.SceneData scene)
        {
            for (int i = 0; i < scene.ObjectsLength; i++)
            {
                var fObject = scene.GetObjects(i);

                var gameObject = new GameObjectData(fObject.Id);
                foreach (var prop in DeserializeProperties(fObject))
                    gameObject.AddVariable(prop);

                yield return gameObject;
            }
        }

        private IEnumerable<VariableData> DeserializeProperties(Schema.GameObjectData gameObject)
        {
            for (int i = 0; i < gameObject.PropertiesLength; i++)
            {
                var fVariable = gameObject.GetProperties(i);

                var variable = new VariableData(fVariable.Id);
                foreach (var frame in DeserializeFrames(fVariable))
                    variable.AddFrame(frame);

                yield return variable;
            }
        }

        private IEnumerable<KeyFrameData> DeserializeFrames(Schema.VariableData variable)
        {
            for (int i = 0; i < variable.KeyFramesLength; i++)
            {
                var fFrame = variable.GetKeyFrames(i);

                switch (fFrame.DataType)
                {
                    case Schema.KeyFrameUnion.LinearKeyFrameData_Int16:
                        {
                            var frame = fFrame.GetData<Schema.LinearKeyFrameData_Int16>(new Schema.LinearKeyFrameData_Int16());

                            yield return new LinearKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.LinearKeyFrameData_Int32:
                        {
                            var frame = fFrame.GetData<Schema.LinearKeyFrameData_Int32>(new Schema.LinearKeyFrameData_Int32());

                            yield return new LinearKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.LinearKeyFrameData_Int64:
                        {
                            var frame = fFrame.GetData<Schema.LinearKeyFrameData_Int64>(new Schema.LinearKeyFrameData_Int64());

                            yield return new LinearKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.LinearKeyFrameData_Float:
                        {
                            var frame = fFrame.GetData<Schema.LinearKeyFrameData_Float>(new Schema.LinearKeyFrameData_Float());

                            yield return new LinearKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.PulseKeyFrameData_Int16:
                        {
                            var frame = fFrame.GetData<Schema.PulseKeyFrameData_Int16>(new Schema.PulseKeyFrameData_Int16());

                            yield return new PulseKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.PulseKeyFrameData_Int32:
                        {
                            var frame = fFrame.GetData<Schema.PulseKeyFrameData_Int32>(new Schema.PulseKeyFrameData_Int32());

                            yield return new PulseKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.PulseKeyFrameData_Int64:
                        {
                            var frame = fFrame.GetData<Schema.PulseKeyFrameData_Int64>(new Schema.PulseKeyFrameData_Int64());

                            yield return new PulseKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.PulseKeyFrameData_Float:
                        {
                            var frame = fFrame.GetData<Schema.PulseKeyFrameData_Float>(new Schema.PulseKeyFrameData_Float());

                            yield return new PulseKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.StepKeyFrameData_Boolean:
                        {
                            var frame = fFrame.GetData<Schema.StepKeyFrameData_Boolean>(new Schema.StepKeyFrameData_Boolean());

                            yield return new StepKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.StepKeyFrameData_Int16:
                        {
                            var frame = fFrame.GetData<Schema.StepKeyFrameData_Int16>(new Schema.StepKeyFrameData_Int16());

                            yield return new StepKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.StepKeyFrameData_Int32:
                        {
                            var frame = fFrame.GetData<Schema.StepKeyFrameData_Int32>(new Schema.StepKeyFrameData_Int32());

                            yield return new StepKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.StepKeyFrameData_Int64:
                        {
                            var frame = fFrame.GetData<Schema.StepKeyFrameData_Int64>(new Schema.StepKeyFrameData_Int64());

                            yield return new StepKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    case Schema.KeyFrameUnion.StepKeyFrameData_Float:
                        {
                            var frame = fFrame.GetData<Schema.StepKeyFrameData_Float>(new Schema.StepKeyFrameData_Float());

                            yield return new StepKeyFrameData(frame.Milliseconds, frame.Value);
                        }
                        break;

                    default:
                        throw new NotSupportedException("Unknown type of keyFrame");
                }
            }
        }

        #endregion
    }
}
