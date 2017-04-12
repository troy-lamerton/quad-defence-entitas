//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TargetPositionComponent targetPosition { get { return (TargetPositionComponent)GetComponent(GameComponentsLookup.TargetPosition); } }
    public bool hasTargetPosition { get { return HasComponent(GameComponentsLookup.TargetPosition); } }

    public void AddTargetPosition(float newX, float newY, float newZ) {
        var index = GameComponentsLookup.TargetPosition;
        var component = CreateComponent<TargetPositionComponent>(index);
        component.x = newX;
        component.y = newY;
        component.z = newZ;
        AddComponent(index, component);
    }

    public void ReplaceTargetPosition(float newX, float newY, float newZ) {
        var index = GameComponentsLookup.TargetPosition;
        var component = CreateComponent<TargetPositionComponent>(index);
        component.x = newX;
        component.y = newY;
        component.z = newZ;
        ReplaceComponent(index, component);
    }

    public void RemoveTargetPosition() {
        RemoveComponent(GameComponentsLookup.TargetPosition);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.MatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTargetPosition;

    public static Entitas.IMatcher<GameEntity> TargetPosition {
        get {
            if(_matcherTargetPosition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TargetPosition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTargetPosition = matcher;
            }

            return _matcherTargetPosition;
        }
    }
}
