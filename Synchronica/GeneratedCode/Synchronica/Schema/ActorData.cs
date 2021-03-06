// automatically generated, do not modify

namespace Synchronica.Schema
{

using FlatBuffers;

public sealed class ActorData : Table {
  public static ActorData GetRootAsActorData(ByteBuffer _bb) { return GetRootAsActorData(_bb, new ActorData()); }
  public static ActorData GetRootAsActorData(ByteBuffer _bb, ActorData obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public ActorData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Id { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public ActorEventData GetEvents(int j) { return GetEvents(new ActorEventData(), j); }
  public ActorEventData GetEvents(ActorEventData obj, int j) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int EventsLength { get { int o = __offset(6); return o != 0 ? __vector_len(o) : 0; } }
  public VariableData GetVariables(int j) { return GetVariables(new VariableData(), j); }
  public VariableData GetVariables(VariableData obj, int j) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int VariablesLength { get { int o = __offset(8); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<ActorData> CreateActorData(FlatBufferBuilder builder,
      int id = 0,
      VectorOffset events = default(VectorOffset),
      VectorOffset variables = default(VectorOffset)) {
    builder.StartObject(3);
    ActorData.AddVariables(builder, variables);
    ActorData.AddEvents(builder, events);
    ActorData.AddId(builder, id);
    return ActorData.EndActorData(builder);
  }

  public static void StartActorData(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddEvents(FlatBufferBuilder builder, VectorOffset eventsOffset) { builder.AddOffset(1, eventsOffset.Value, 0); }
  public static VectorOffset CreateEventsVector(FlatBufferBuilder builder, Offset<ActorEventData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartEventsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddVariables(FlatBufferBuilder builder, VectorOffset variablesOffset) { builder.AddOffset(2, variablesOffset.Value, 0); }
  public static VectorOffset CreateVariablesVector(FlatBufferBuilder builder, Offset<VariableData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartVariablesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<ActorData> EndActorData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<ActorData>(o);
  }
};


}
