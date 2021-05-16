using System.Collections.Generic;
using System;

// Criação da class usada para as peças dos jogadores

namespace OlaMundo {

    class Player {

        // Variável com as peças do jogador(7 peças)
        int[] piecesSlots = new int[7];

        public Player() {
            for(int i = 0; i < 7; i++) {
                piecesSlots[i] = 0;
            }
        }

        // Junta a peça escolhida e o slot onde vai parar
        public void setPieceNewSlot(int slot, int jumps) {
            int piece = getPieceInSlot(slot);
            piecesSlots[piece] = piecesSlots[piece] + jumps;
            Console.WriteLine("[!] A peça do " + slot + " foi parar ao " + (slot + jumps) + ".");
        }

        // Faz a peça mover-se para onde o jogador quer
        public int getPieceInSlot(int slot) {
            int piece = -1;
            for (int i = 0; i<7; i++) {
                if (piecesSlots[i] == slot) {
                    piece = i;
                    break;
                }
            }
            return piece;
        }
        
        // Condições para o sitio onde a peça vai
        public Boolean slotHasPiece(int slot) {
            if (getPieceInSlot(slot) == -1) return false;
            return true;
        }

        // Verificar se o jogador tem alguma peça que não esteja no final
        public Boolean hasAllFinished() {
            for (int i = 0; i<7; i++) {
                if (piecesSlots[i] != 15) return false;
            }
            return true;
        }

    }

}