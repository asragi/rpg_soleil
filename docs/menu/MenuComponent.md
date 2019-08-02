# MenuComponent
複数の[IComponent](Icomponent.md)を親子関係として扱うためのクラス．
これ自身が[IComponent](Icomponent.md)を実装している．

## Method

### AddComponent
IComponentのインスタンスの配列を渡す．MenuComponentの`Update()`，`Draw()`，`Call()`，`Quit()`が呼ばれた時に渡したインスタンスのそれらも呼ばれるようになる．

```csharp
class TestComponent: MenuComponent{
    Image img1, img2;
    public TestComponent(){
        img1 = new Image();
        img2 = new Image();
    }

    public override void Update(){
        base.Update();
        img1.Update();
        img2.Update();
    }

    public override void Call(){
        base.Call();
        img1.Call();
        img2.Call();
    }

    public override void Quit(){
        base.Quit();
        img1.Quit();
        img2.Quit();
    }

    public override void Draw(Drawing d){
        base.Draw(d);
        img1.Draw(d);
        img2.Draw(d);
    }
}
```

と書く必要がある部分を，

```csharp
class TestComponent: MenuComponent{
    Image img1, img2;
    public TestComponent(){
        img1 = new Image();
        img2 = new Image();
        AddComponent(new [] { img1, img2 })
    }
}
```

と書くことで同等の処理を実現できる．
