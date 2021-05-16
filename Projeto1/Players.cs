using System.Collections.Generic;
using System;

//Criação dos 2 jogadores

namespace OlaMundo{

    class Player {

        //Cada jogador tem 7 peças
        int[] piecesSlots = new int[7];

        public Player() {
            for(int i = 0; i < 7; i++) {
                piecesSlots[i] = 0;
            }
        }

        //Junta a peça escolhida e o slot onde vai parar
        public void setPieceNewSlot(int slot, int jumps) {
            int piece = getPieceInSlot(slot);
            piecesSlots[piece] = piecesSlots[piece] + jumps;
            Console.WriteLine("[!] A peça do " + slot + " foi parar ao " + (slot + jumps) + ".");
        }

        //Faz a peça mover-se para onde o jogador quer
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
        
        //Condições para o sitio onde a peça vai
        public Boolean slotHasPiece(int slot) {
            if (getPieceInSlot(slot) == -1) return false;
            return true;
        }
        
        public int[] getAllPieces() {
            return piecesSlots;
        }

        public Boolean hasAllFinished() {
            for (int i = 0; i<7; i++) {
                if (piecesSlots[i] != 15) return false;
            }
            return true;
        }

    }

}