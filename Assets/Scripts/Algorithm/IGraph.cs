using System.Collections.Generic;

/**
 * An abstract graph.
 * It does not use any memory.
 * It has two single abstract function Neighbors, and Edges that returns the
 * neighbors and edges of a given node, respectively.
 * T = type of node in graph.
 * @author Erel Segal-Halevi
 * @since 2020-12
 */
public interface IGraph<T> {
    IEnumerable<T> Neighbors(T node);
    IEnumerable<Edge> Edges(T node);
}
