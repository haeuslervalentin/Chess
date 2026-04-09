# Chess Engine – OOP Plan (C#)

## Überblick

Objektorientiertes Schachspiel in C#. Das Spielfeld ist ein 8×8 Array, Figuren werden als Klassen mit Vererbung modelliert.

---

## Enums

`PieceColor` – `White`, `Black`

`PieceType` – `Pawn`, `Rook`, `Knight`, `Bishop`, `Queen`, `King`

`MoveResult` – `Valid`, `Invalid`, `OutOfBounds`, `Capture`, `Check`

---

## Vererbungshierarchie

```
ChessFigure (abstrakt)
├── Queen
├── Bishop
├── Knight
├── Pawn     (isMoved)
├── Rook     (isMoved)
└── King     (isMoved)
```

`isMoved` wird nur bei `Pawn`, `Rook` und `King` benötigt, da nur diese Sonderregeln (En passant, Rochade) haben, die davon abhängen.

---

## ChessFigure – Abstrakte Basisklasse

**Properties:**
- `PieceColor Color`
- `PieceType Type`
- `Position CurrentPos`
- `bool HasMoved`
- `Position[] ValidMoves`
- `Position[] Hits`

**Methoden:**
- `GetValidMoves(Board)` – abstract, gibt alle legalen Zielfelder zurück
- `CanMoveTo(Position)` – prüft ob Zielposition erreichbar ist (ruft GetValidMoves auf)
- `MoveTo(Position)` – führt Zug aus, aktualisiert Position und HasMoved
- `ToString()` – z.B. `"♛ Black"`

---

## Figuren-Klassen

### Queen
Bewegt sich diagonal und in Linien, unbegrenzte Reichweite.

### Rook
Bewegt sich horizontal und vertikal. `isMoved` für Rochade relevant.

### Knight
L-Form, einzige Figur die andere überspringen kann.

### Bishop
Nur diagonal, unbegrenzte Reichweite.

### Pawn
`isMoved` für Doppelschritt und En passant. Promotion bei Erreichen der letzten Reihe.

### King
`isMoved` für Rochade. Enthält Check-Logik, darf nicht auf bedrohtes Feld ziehen.

---

## Board

**Properties:**
- `ChessFigure?[8,8] Grid` – das Spielfeld als 2D-Array

**Methoden:**
- `GetFigureAt(Position)` – gibt Figur oder null zurück
- `IsEmpty(Position)` – prüft ob Feld leer ist
- `IsInCheck(PieceColor)` – prüft ob König der Farbe im Schach steht
- `Print()` – gibt das Board als Text aus

---

## Game

**Properties:**
- `Board Board`
- `PieceColor CurrentTurn`
- `bool IsCheckmate`
- `bool IsStalemate`

**Methoden:**
- `MakeMove(Position from, Position to)` – führt Zug aus, prüft Gültigkeit, wechselt Spieler
- `GetMovePreview(Position)` – gibt alle gültigen Zielfelder zurück ohne Zug auszuführen
- `ToString()` – Spielzustand als Text (delegiert an Board)

---

## Spezialregeln (Erweiterbar)

- **Rochade** – King und Rook haben `isMoved == false`, kein Feld zwischen ihnen besetzt oder bedroht
- **En passant** – Pawn schlägt diagonal auf leerем Feld nach Doppelschritt des Gegners
- **Promotion** – Pawn erreicht letzte Reihe, wird zur wählbaren Figur
- **Schach / Schachmatt / Remis** – in `Game` geprüft nach jedem Zug

---

## Projektstruktur

```
Chess/
├── Enums.cs
├── Position.cs
├── ChessFigure.cs
├── Pieces/
│   ├── Queen.cs
│   ├── Rook.cs
│   ├── Knight.cs
│   ├── Bishop.cs
│   ├── Pawn.cs
│   └── King.cs
├── Board.cs
└── Game.cs
```
