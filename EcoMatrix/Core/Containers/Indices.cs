namespace EcoMatrix.Core.Containers
{
    public struct Indices
    {
        public uint FirstIndex { get; set; }
        public uint SecondIndex { get; set; }
        public uint ThirdIndex { get; set; }

        public Indices(uint firstIndex, uint secondIndex, uint thirdIndex)
        {
            FirstIndex = firstIndex;
            SecondIndex = secondIndex;
            ThirdIndex = thirdIndex;
        }

        public static Indices operator +(Indices indicesA, Indices indicesB)
        {
            return new Indices(indicesA.FirstIndex + indicesB.FirstIndex,
                               indicesA.SecondIndex + indicesB.SecondIndex,
                               indicesA.ThirdIndex + indicesB.ThirdIndex);
        }

        public static Indices operator -(Indices indicesA, Indices indicesB)
        {
            return new Indices(indicesA.FirstIndex - indicesB.FirstIndex,
                               indicesA.SecondIndex - indicesB.SecondIndex,
                               indicesA.ThirdIndex - indicesB.ThirdIndex);
        }

        public static Indices operator *(Indices indicesA, Indices indicesB)
        {
            return new Indices(indicesA.FirstIndex * indicesB.FirstIndex,
                               indicesA.SecondIndex * indicesB.SecondIndex,
                               indicesA.ThirdIndex * indicesB.ThirdIndex);
        }

        public static Indices operator /(Indices indicesA, Indices indicesB)
        {
            return new Indices(indicesA.FirstIndex / indicesB.FirstIndex,
                               indicesA.SecondIndex / indicesB.SecondIndex,
                               indicesA.ThirdIndex / indicesB.ThirdIndex);
        }

        public static Indices operator %(Indices indicesA, Indices indicesB)
        {
            return new Indices(indicesA.FirstIndex % indicesB.FirstIndex,
                               indicesA.SecondIndex % indicesB.SecondIndex,
                               indicesA.ThirdIndex % indicesB.ThirdIndex);
        }
    }
}