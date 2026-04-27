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


# 🧠 Chess Engine – To-Do Liste

## ✅ 1. Zug-System (Turn Handling)
- [ ] `CurrentTurn` Variable einführen
  - Speichert, ob Weiß oder Schwarz am Zug ist
- [ ] Im `Move()` prüfen:
  - Spieler darf nur eigene Figuren bewegen
- [ ] Nach erfolgreichem Zug:
  - Turn wechseln

---

## ♟️ 2. Pawn verbessern
- [ ] 2-Felder Startzug implementieren
  - Nur wenn `isMoved == false`
  - Beide Felder müssen frei sein
- [ ] Bug fixen (Bounds-Check)
  - Aktuell bricht Funktion zu früh ab (`return moves`)
- [ ] Diagonales Schlagen korrekt behandeln
- [ ] (Optional) En Passant implementieren
- [ ] (Optional) Promotion hinzufügen
  - Bauer wird zu Dame, Turm, Läufer oder Springer

---

## ⚠️ 3. Move-Validierung verbessern
- [ ] Ungültige Züge abfangen
  - Exception werfen statt „nichts tun“
- [ ] Prüfen:
  - Zielposition im `availableMoves` enthalten?

---

## 👑 4. König absichern (Check verhindern)
- [ ] Methode erstellen:
  - `IsFieldUnderAttack(row, col, color)`
- [ ] König darf NICHT:
  - auf bedrohte Felder ziehen
- [ ] Vor jedem Zug prüfen:
  - Führt der Zug zu eigenem Schach?

---

## 🔍 5. Check erkennen
- [ ] Methode:
  - `IsKingInCheck(color)`
- [ ] Ablauf:
  - König finden
  - Alle gegnerischen Züge berechnen
  - Prüfen, ob König angegriffen wird

---

## ☠️ 6. Schachmatt erkennen
- [ ] Wenn König im Schach ist:
  - Prüfen, ob irgendein Zug das verhindert
- [ ] Falls kein legaler Zug möglich:
  - → Schachmatt

---

## 🏰 7. Rochade (Castling)
- [ ] Bedingungen prüfen:
  - König & Turm haben sich nicht bewegt
  - Felder dazwischen sind frei
  - König steht nicht im Schach
  - König läuft nicht durch bedrohte Felder
- [ ] Speziellen Move implementieren

---

## 🧱 8. Architektur verbessern
- [ ] Neue Klasse `Game` erstellen
  - Verwaltet:
    - Turn
    - Regeln
    - Spielstatus
- [ ] `Board` nur für Zustand verwenden

---

## 🧪 9. Debugging & Anzeige
- [ ] Brett mit Koordinaten anzeigen (A–H, 1–8)
- [ ] Züge visuell markieren (hast du schon 👍)
- [ ] Eingabe-System bauen (z.B. "e2 e4")

---

## 🎯 Bonus Features (Optional)
- [ ] Remis-Regeln (Stalemate, 50-Züge-Regel)
- [ ] Undo/Redo Funktion
- [ ] Spiel speichern/laden
- [ ] Einfache KI (Random oder Minimax)

---

## 🚀 Empfohlene Reihenfolge
1. Turn-System
2. Pawn fixen
3. Move-Validierung
4. Check erkennen
5. King Safety
6. Schachmatt
7. Rochade
8. Bonus Features